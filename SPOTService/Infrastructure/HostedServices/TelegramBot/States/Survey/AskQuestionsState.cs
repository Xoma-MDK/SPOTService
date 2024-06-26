﻿using Microsoft.EntityFrameworkCore;
using SPOTService.DataStorage;
using SPOTService.DataStorage.Entities;
using SPOTService.Infrastructure.HostedServices.TelegramBot.AbstractClass;
using SPOTService.Infrastructure.HostedServices.TelegramBot.Interfaces;
using SPOTService.Infrastructure.HostedServices.TelegramBot.States.Menu;
using System.Diagnostics;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using SurveyEntity = SPOTService.DataStorage.Entities.Survey;
namespace SPOTService.Infrastructure.HostedServices.TelegramBot.States.Survey
{
    /// <summary>
    /// Состояние опроса, в котором происходит задание вопросов пользователю в Telegram боте.
    /// </summary>
    public class AskQuestionsState : AAsyncState, IAsyncState
    {
        private readonly SurveyEntity? _surveyEntity;
        private readonly Dictionary<Question, bool>? _questions;
        private Question? _questionCurrent;

        /// <summary>
        /// Конструктор состояния AskQuestionsState для начала опроса.
        /// </summary>
        /// <param name="serviceScope">Область провайдера сервисов.</param>
        /// <param name="survey">Сущность опроса.</param>
        public AskQuestionsState(IServiceProvider serviceScope, SurveyEntity survey)
        {
            _serviceScope = serviceScope;
            _surveyEntity = survey;
            _questions = _surveyEntity.Questions!
                .ToDictionary(question => question, _ => false);
        }
        /// <summary>
        /// Возвращает текущий вопрос для задания.
        /// </summary>
        /// <returns>Текущий вопрос.</returns>
        private Question? GetQuestion()
        {
            return _questions!.FirstOrDefault(q => q.Value == false).Key;
        }

        /// <summary>
        /// Конструктор состояния AskQuestionsState для начала опроса.
        /// </summary>
        /// <param name="serviceScope">Область провайдера сервисов.</param>
        /// <param name="botClient">Клиент Telegram Bot API.</param>
        /// <param name="stateMachine">Машина состояний для управления состояниями.</param>
        public AskQuestionsState(IServiceProvider serviceScope, TelegramBotClient botClient, IAsyncStateMachine stateMachine)
        {
            _botClient = botClient;
            _stateMachine = stateMachine;
            _userId = _stateMachine.UserId;
            _chatId = _stateMachine.ChatId;
            _serviceScope = serviceScope;
        }

        /// <inheritdoc/>
        public async Task EnterAsync(TelegramBotClient botClient, IAsyncStateMachine stateMachine)
        {
            _botClient = botClient;
            _stateMachine = stateMachine;
            _userId = _stateMachine.UserId;
            _chatId = _stateMachine.ChatId;
            _questionCurrent = GetQuestion()!;
            await SendQuestion(_questionCurrent);


        }
        /// <summary>
        /// Отправляет вопрос пользователю в Telegram.
        /// </summary>
        /// <param name="question">Вопрос для отправки.</param>
        private async Task SendQuestion(Question question)
        {
            if (question.IsOpen)
            {
                await _botClient.SendTextMessageAsync(_chatId, question.Title + "\n\nЭто открытый вопрос, напишите свой ответ в сообщении и отправьте его");
            }
            else
            {
                using var scope = _serviceScope.CreateScope();
                using var mainContext = scope.ServiceProvider.GetRequiredService<MainContext>();
                question = mainContext.Questions.Include(q => q.AnswerVariants).Where(q => q.Id == question.Id).First();
                var options = question.AnswerVariants!.ToList();
                var buttons = options.Select(o => InlineKeyboardButton.WithCallbackData(o.Title, $"Question:{question.Id};AnswerVariant:{o.Id}"));
                var keyboard = new InlineKeyboardMarkup(buttons);
                await _botClient.SendTextMessageAsync(_chatId, question.Title, replyMarkup: keyboard);
            }
        }

        /// <inheritdoc/>
        public async Task ExecuteAsync(Message message)
        {
            if (_questionCurrent != null && _questionCurrent.IsOpen == false)
            {
                await _botClient.SendTextMessageAsync(_chatId, "Извините, этот вопрос только с вариантами ответов!");
            }
            else
            {
                using var scope = _serviceScope.CreateScope();
                using var mainContext = scope.ServiceProvider.GetRequiredService<MainContext>();
                var respondent = mainContext.Respondents.First(r => r.TelegramId == _userId);
                var answer = new Answer()
                {
                    SurveyId = _surveyEntity!.Id,
                    QuestionId = _questionCurrent!.Id,
                    AnswerVariantId = null,
                    RespondentId = respondent.Id,
                    OpenAnswer = message.Text
                };
                try
                {
                    await mainContext.AddAsync(answer);
                    await mainContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message, ex.InnerException!.Message);
                }
                _questions![_questionCurrent] = true;
                _questionCurrent = GetQuestion();
                if (_questionCurrent != null)
                {
                    await SendQuestion(_questionCurrent);
                }
                else
                {
                    await _botClient.SendTextMessageAsync(_chatId, "Спасибо за прохождение опроса!");
                    await _stateMachine.ChangeStateAsync(new MainMenuState(_stateMachine.ServiceScope));
                }
            }
        }

        /// <inheritdoc/>
        public async Task ExecuteAsync(CallbackQuery query)
        {
            try
            {
                if (query.Data != null && query.Data.StartsWith("Question"))
                {
                    using var scope = _serviceScope.CreateScope();
                    using var mainContext = scope.ServiceProvider.GetRequiredService<MainContext>();
                    await _botClient.AnswerCallbackQueryAsync(query.Id);
                    var answerVariantId = int.Parse(query.Data.Split(';')[1].Split(':')[1]);
                    var questionId = int.Parse(query.Data.Split(';')[0].Split(':')[1]);
                    var respondent = mainContext!.Respondents.First(r => r.TelegramId == _userId);
                    if (_questionCurrent != null && _questionCurrent.Id != questionId || _questionCurrent == null)
                    {
                        return;
                    }
                    var answer = new Answer()
                    {
                        SurveyId = _surveyEntity!.Id,
                        QuestionId = questionId,
                        AnswerVariantId = answerVariantId,
                        RespondentId = respondent.Id,
                        OpenAnswer = null
                    };
                    await mainContext.AddAsync(answer);
                    await mainContext.SaveChangesAsync();
                    _questions![_questionCurrent] = true;
                    _questionCurrent = GetQuestion();
                    if (_questionCurrent != null)
                    {
                        await SendQuestion(_questionCurrent);
                    }
                    else
                    {

                        await _botClient.SendTextMessageAsync(_chatId, "Спасибо за прохождение опроса!");
                        await _stateMachine.ChangeStateAsync(new MainMenuState(_stateMachine.ServiceScope));
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        /// <inheritdoc/>
        public Task ExitAsync()
        {
            return Task.CompletedTask;
        }
    }
}
