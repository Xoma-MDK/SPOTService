using SPOTService.DataStorage;
using SPOTService.Infrastructure.HostedServices.TelegramBot.AbstractClass;
using SPOTService.Infrastructure.HostedServices.TelegramBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SPOTService.Infrastructure.HostedServices.TelegramBot.States.Register
{
    public class CancelRegisterState(MainContext mainContext) : AAsyncState, IAsyncState
    {
        public async Task EnterAsync(TelegramBotClient botClient, IAsyncStateMachine stateMachine)
        {
            _botClient = botClient;
            _stateMachine = stateMachine;
            _userId = _stateMachine.UserId;
            _chatId = _stateMachine.ChatId;
            _mainContext = mainContext;
        }

        public Task ExecuteAsync(Message message)
        {
            throw new NotImplementedException();
        }

        public Task ExecuteAsync(CallbackQuery query)
        {
            throw new NotImplementedException();
        }

        public Task ExitAsync()
        {
            throw new NotImplementedException();
        }
    }
}
