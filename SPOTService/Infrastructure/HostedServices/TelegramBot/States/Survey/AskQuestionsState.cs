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
    public class AskQuestionsState : AAsyncState, IAsyncState
    {
        private SurveyEntity _surveyEntity;
        private Dictionary<Question, bool> _questions;
        private Question? _questionCurrent;
        public AskQuestionsState(MainContext mainContext, SurveyEntity survey)
        {
            _mainContext = mainContext;
            _surveyEntity = survey;
            _questions = _surveyEntity.Questions!
                .ToDictionary(question => question, _ => false);
        }
        private Question? GetQuestion()
        {
            return _questions.FirstOrDefault(q => q.Value == false).Key;
        }
        public AskQuestionsState(MainContext mainContext, TelegramBotClient botClient, IAsyncStateMachine stateMachine)
        {
            _mainContext = mainContext;
            _botClient = botClient;
            _stateMachine = stateMachine;
            _userId = _stateMachine.UserId;
            _chatId = _stateMachine.ChatId;
        }
        public async Task EnterAsync(TelegramBotClient botClient, IAsyncStateMachine stateMachine)
        {
            _botClient = botClient;
            _stateMachine = stateMachine;
            _userId = _stateMachine.UserId;
            _chatId = _stateMachine.ChatId;
            _questionCurrent = GetQuestion()!;
            await SendQuestion(_questionCurrent);


        }
        private async Task SendQuestion(Question question)
        {
            if (question.IsOpen)
            {
                await _botClient.SendTextMessageAsync(_chatId, question.Title);
            }
            else
            {
                var options = question.AnswerVariants!.ToList();
                var buttons = options.Select(o => InlineKeyboardButton.WithCallbackData(o.Title, $"AnswerVariant:{o.Id}"));
                var keyboard = new InlineKeyboardMarkup(buttons);
                await _botClient.SendTextMessageAsync(_chatId, question.Title, replyMarkup: keyboard);
            }
        }
        public async Task ExecuteAsync(Message message)
        {
            if (_questionCurrent.IsOpen == false)
            {
                await _botClient.SendTextMessageAsync(_chatId, "Извините, этот вопрос только с вариантами ответов!");
            }
            else
            {
                var respondent = _mainContext.Respondents.First(r => r.TelegramId == _userId);
                var answer = new Answer()
                {
                    SurveyId = _surveyEntity.Id,
                    QuestionId = _questionCurrent!.Id,
                    AnswerVariantId = null,
                    RespondentId = respondent.Id,
                    OpenAnswer = message.Text
                };
                try
                {
                    await _mainContext.AddAsync(answer);
                    await _mainContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message, ex.InnerException!.Message);
                }
                _questions[_questionCurrent] = true;
                _questionCurrent = GetQuestion();
                if (_questionCurrent != null)
                {
                    await SendQuestion(_questionCurrent);
                }
                else
                {
                    await _botClient.SendTextMessageAsync(_chatId, "Спасибо за прохождение опроса!");
                    await _stateMachine.ChangeStateAsync(new MainMenuState(_mainContext));
                }
            }
        }

        public async Task ExecuteAsync(CallbackQuery query)
        {
            try
            {
                if (query.Data!.StartsWith("AnswerVariant"))
                {
                    await _botClient.AnswerCallbackQueryAsync(query.Id);
                    var answerVariantId = int.Parse(query.Data!.Split(':')[1]);
                    var respondent = _mainContext.Respondents.First(r => r.TelegramId == _userId);
                    var answer = new Answer()
                    {
                        SurveyId = _surveyEntity.Id,
                        QuestionId = _questionCurrent!.Id,
                        AnswerVariantId = answerVariantId,
                        RespondentId = respondent.Id,
                        OpenAnswer = null
                    };
                    await _mainContext.AddAsync(answer);
                    await _mainContext.SaveChangesAsync();
                    _questions[_questionCurrent] = true;
                    _questionCurrent = GetQuestion();
                    if (_questionCurrent != null)
                    {
                        await SendQuestion(_questionCurrent);
                    }
                    else
                    {

                        await _botClient.SendTextMessageAsync(_chatId, "Спасибо за прохождение опроса!");
                        await _stateMachine.ChangeStateAsync(new MainMenuState(_mainContext));
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, ex.InnerException!.Message);
            }
        }

        public async Task ExitAsync()
        {

        }
    }
}
