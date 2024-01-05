using SPOTService.DataStorage;
using SPOTService.DataStorage.Entities;
using SPOTService.Infrastructure.HostedServices.TelegramBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace SPOTService.Infrastructure.HostedServices.TelegramBot.States
{
    public class WaitingEnterPatronomycState(MainContext mainContext) : IAsyncState
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
            var inlineKeyboard = new InlineKeyboardMarkup
            (
                new List<InlineKeyboardButton[]>()
                    {
                        new InlineKeyboardButton[]
                        {
                             InlineKeyboardButton.WithCallbackData("У меня нет отчества", "NonePatronomyc"),
                        },
                    }
            );
            await _botClient.SendTextMessageAsync(_chatId, "Записал!\n" +
                "Осталось отчество, если оно есть у тебя, то напиши мне.\n" +
                "Если его нету, то нажми кнопку ниже.",
                replyMarkup: inlineKeyboard);
        }

        public async Task ExecuteAsync(Message message)
        {
            if (message.Text != "")
            {
                var respondent = _mainContext.Respondents.First(r => r.TelegramId == _userId);
                if (respondent != null)
                {
                    respondent.Patronomic = message.Text;
                    _mainContext.Update(respondent);
                    await _mainContext.SaveChangesAsync();
                }
                await _stateMachine.ChangeStateAsync(new EndingForRegisterState(_stateMachine.MainContext));
            }
            else
            {
                await _botClient.SendTextMessageAsync(_chatId, "Фамилия не может быть пустой!");
            }
        }

        public async Task ExecuteAsync(CallbackQuery query)
        {
            if (query.Data == "NonePatronomyc")
            {
                await _stateMachine.ChangeStateAsync(new EndingForRegisterState(_stateMachine.MainContext));
            }
        }

        public async Task ExitAsync()
        {
        }
    }
}
