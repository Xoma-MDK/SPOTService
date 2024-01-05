using SPOTService.Infrastructure.HostedServices.TelegramBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SPOTService.Infrastructure.HostedServices.TelegramBot.States
{
    public class WaitingForCodeState : IAsyncState
    {
        private TelegramBotClient _botClient;
        public async Task EnterAsync(TelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        public Task EnterAsync(TelegramBotClient botClient, IAsyncStateMachine stateMachine)
        {
            throw new NotImplementedException();
        }

        public async Task ExecuteAsync(Message message)
        {
            throw new NotImplementedException();
        }

        public async Task ExecuteAsync(CallbackQuery query)
        {
            throw new NotImplementedException();
        }

        public async Task ExitAsync()
        {
            throw new NotImplementedException();
        }
    }
}
