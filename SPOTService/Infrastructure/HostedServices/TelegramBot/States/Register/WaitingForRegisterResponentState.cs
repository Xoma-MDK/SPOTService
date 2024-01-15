using SPOTService.DataStorage;
using SPOTService.Infrastructure.HostedServices.TelegramBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using SPOTService.Infrastructure.HostedServices.TelegramBot.AbstractClass;
using SPOTService.Infrastructure.HostedServices.TelegramBot.States.Menu;

namespace SPOTService.Infrastructure.HostedServices.TelegramBot.States.Register
{
    public class WaitingForRegisterResponentState(IServiceProvider serviceScope) : AAsyncState, IAsyncState
    {
        public async Task EnterAsync(TelegramBotClient botClient, IAsyncStateMachine stateMachine)
        {
            _botClient = botClient;
            _stateMachine = stateMachine;
            _userId = _stateMachine.UserId;
            _chatId = _stateMachine.ChatId;
            _serviceScope = serviceScope;
            using var scope = _serviceScope.CreateScope();
            using var mainContext = scope.ServiceProvider.GetRequiredService<MainContext>();
            if (mainContext.Respondents.Any(x => x.TelegramId == _userId))
            {
                await _stateMachine.ChangeStateAsync(new MainMenuState(_stateMachine.ServiceScope));
            }
            else
            {
                var inlineKeyboard = new InlineKeyboardMarkup(
                   new List<InlineKeyboardButton[]>()
                   {
                                        new InlineKeyboardButton[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("Зарегистрироваться!", "reg"),
                                            InlineKeyboardButton.WithCallbackData("Анонимно!", "anon"),
                                        },
                   });
                await _botClient.SendTextMessageAsync(
                    _chatId,
                    "Хотите ли вы зарегистрироваться?\n" +
                    "Или желаете пройти опрос анонимно?",
                    replyMarkup: inlineKeyboard);
                await _stateMachine.ChangeStateAsync(new RegisterResponentState(_stateMachine.ServiceScope));
            }
        }

        public async Task ExecuteAsync(Message message)
        {
        }

        public async Task ExecuteAsync(CallbackQuery query)
        {

        }

        public async Task ExitAsync()
        {
        }
    }
}
