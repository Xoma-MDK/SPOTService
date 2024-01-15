using SPOTService.DataStorage;
using SPOTService.Infrastructure.HostedServices.TelegramBot.AbstractClass;
using SPOTService.Infrastructure.HostedServices.TelegramBot.Interfaces;
using SPOTService.Infrastructure.HostedServices.TelegramBot.States.Register;
using SPOTService.Infrastructure.HostedServices.TelegramBot.States.Survey;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace SPOTService.Infrastructure.HostedServices.TelegramBot.States.Menu
{
    public class MainMenuState : AAsyncState, IAsyncState
    {
        public MainMenuState(IServiceProvider serviceScope)
        {
            _serviceScope = serviceScope;
        }
        public MainMenuState(IServiceProvider serviceScope, TelegramBotClient botClient, IAsyncStateMachine stateMachine)
        {
            _botClient = botClient;
            _stateMachine = stateMachine;
            _userId = _stateMachine.UserId;
            _chatId = _stateMachine.ChatId;
            _serviceScope = serviceScope;
        }
        public async Task EnterAsync(TelegramBotClient botClient, IAsyncStateMachine stateMachine)
        {
            _botClient = botClient;
            _stateMachine = stateMachine;
            _userId = _stateMachine.UserId;
            _chatId = _stateMachine.ChatId;
            var replyKeyboard = new ReplyKeyboardMarkup
            (
                new List<KeyboardButton[]>()
                {
                        new KeyboardButton[]
                        {
                             new KeyboardButton("Пройти опрос"),
                        },
                }
            )
            { 
                ResizeKeyboard = true
            };
            await _botClient.SendTextMessageAsync(
                _chatId,
                "Добро пожаловать!\n" +
                "Основное меню находиться в кнопках",
                replyMarkup: replyKeyboard);
        }

        public async Task ExecuteAsync(Message message)
        {
            if (message.Text == "Пройти опрос")
            {
                await _stateMachine.ChangeStateAsync(new WaitingForCodeState(_stateMachine.ServiceScope));
            }
        }

        public async Task ExecuteAsync(CallbackQuery query)
        {
            await _botClient.AnswerCallbackQueryAsync(query.Id);
        }

        public async Task ExitAsync()
        {
        }
    }
}
