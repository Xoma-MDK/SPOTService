using Microsoft.Extensions.DependencyInjection;
using SPOTService.DataStorage;
using Telegram.Bot.Types;

namespace SPOTService.Infrastructure.HostedServices.TelegramBot.Interfaces
{
    public interface IAsyncStateMachine
    {
        IAsyncState CurrentState { get; }
        long UserId { get; }
        long ChatId { get; }
        IServiceProvider ServiceScope { get; }

        Task ChangeStateAsync(IAsyncState newState);
        Task RestoreStateAsync(int id);
        Task ProcessAsync(Message message);
        Task ProcessAsync(CallbackQuery query);
    }
}
