using SPOTService.DataStorage;
using SPOTService.DataStorage.Entities;
using SPOTService.Infrastructure.HostedServices.TelegramBot.AbstractClass;
using SPOTService.Infrastructure.HostedServices.TelegramBot.Interfaces;
using SPOTService.Infrastructure.HostedServices.TelegramBot.States.Menu;
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
            if(_questionCurrent.AnswerVariants != null)
            {
                await SendQuestionWithAnswerVariant(_questionCurrent);
            }


        }
        private async Task SendQuestionWithAnswerVariant(Question question)
        {
            if (question.IsOpen == false)
            {
                var options = question.AnswerVariants!.ToList();
                var buttons = options.Select(o => InlineKeyboardButton.WithCallbackData(o.Title, $"AnswerVariant:{o.Id}"));
                var keyboard = new InlineKeyboardMarkup(buttons);
                await _botClient.SendTextMessageAsync(_chatId, question.Title, replyMarkup: keyboard);
            }
        }
        public async Task ExecuteAsync(Message message)
        {
            if(_questionCurrent.IsOpen == false)
            {
                await _botClient.SendTextMessageAsync(_chatId, "");
            }
        }

        public async Task ExecuteAsync(CallbackQuery query)
        {
            if (query.Data!.StartsWith("AnswerVariant"))
            {
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
                if(_questionCurrent != null) 
                {
                    if (_questionCurrent.AnswerVariants != null) 
                    {
                        await SendQuestionWithAnswerVariant(_questionCurrent);
                    }
                }
                else
                {
                    await _botClient.SendTextMessageAsync(_chatId,"Спасибо за прохождение опроса!");
                    await _stateMachine.ChangeStateAsync(new MainMenuState(_mainContext));
                }

            }
            await _botClient.AnswerCallbackQueryAsync(query.Id);
        }

        public async Task ExitAsync()
        {

        }
    }
}
