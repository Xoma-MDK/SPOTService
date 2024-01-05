using SPOTService.DataStorage.Entities;
using SPOTService.DataStorage;
using SPOTService.Infrastructure.HostedServices.TelegramBot.enums;
using SPOTService.Infrastructure.HostedServices.TelegramBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Telegram.Bot.Types.ReplyMarkups;

namespace SPOTService.Infrastructure.HostedServices.TelegramBot.States
{
    public class EndingForRegisterState(MainContext mainContext) : IAsyncState
    {
        private TelegramBotClient _botClient;
        private IAsyncStateMachine _stateMachine;
        private readonly MainContext _mainContext = mainContext;
        private long _userId;
        private long _chatId;
        public async Task EnterAsync(TelegramBotClient botClient, IAsyncStateMachine stateMachine)
        {
            _botClient = botClient;
            _stateMachine = stateMachine;
            _userId = _stateMachine.UserId;
            _chatId = _stateMachine.ChatId;
            var groups = mainContext.Groups.ToList();
            var buttons = groups.Select(g => InlineKeyboardButton.WithCallbackData(g.Title, $"Group:{g.Id}"));
            var keyboard = new InlineKeyboardMarkup(buttons);
            await _botClient.SendTextMessageAsync(_chatId, "Выбери свою группу:", replyMarkup: keyboard);
        }

        public async Task ExecuteAsync(Message message)
        {
            var groups = mainContext.Groups.ToList();
            var buttons = groups.Select(g => InlineKeyboardButton.WithCallbackData(g.Title, $"Group:{g.Id}"));
            var keyboard = new InlineKeyboardMarkup(buttons);
            await _botClient.SendTextMessageAsync
                (
                _chatId, 
                "Пожалуйста используй кнопки!\n" +
                "Выбери свою группу:",
                replyMarkup: keyboard
                );
        }

        public async Task ExecuteAsync(CallbackQuery query)
        {
            if (query.Data!.StartsWith("Group"))
            {
                var groupId = int.Parse(query.Data.Split(':')[1]);
                var respondent = _mainContext.Respondents.First(r => r.TelegramId == _userId);
                if (respondent != null)
                {
                    respondent.GroupId = groupId;
                    _mainContext.Update(respondent);
                    await _mainContext.SaveChangesAsync();
                }
                else
                {
                    respondent = new Respondent()
                    {
                        GroupId = groupId,
                        TelegramId = _userId
                    };
                    var respondentEntity = await mainContext.AddAsync(respondent);
                    await mainContext.SaveChangesAsync();
                }
                await _botClient.AnswerCallbackQueryAsync(query.Id);
                await _stateMachine.ChangeStateAsync(new SurveyIsReadyState(_stateMachine.MainContext));
            }
        }

        public async Task ExitAsync()
        {
        }
    }
}
