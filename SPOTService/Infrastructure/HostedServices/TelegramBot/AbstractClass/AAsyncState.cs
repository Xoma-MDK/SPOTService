using Microsoft.Extensions.DependencyInjection;
using SPOTService.DataStorage;
using SPOTService.Infrastructure.HostedServices.TelegramBot.Interfaces;
using Telegram.Bot;

namespace SPOTService.Infrastructure.HostedServices.TelegramBot.AbstractClass
{
    public class AAsyncState
    {
        protected TelegramBotClient _botClient;
        protected IAsyncStateMachine _stateMachine;
        protected IServiceProvider _serviceScope;
        protected long _userId;
        protected long _chatId;
    }
}
