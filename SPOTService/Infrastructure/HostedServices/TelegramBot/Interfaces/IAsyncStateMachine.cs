using SPOTService.DataStorage;
using Telegram.Bot.Types;

namespace SPOTService.Infrastructure.HostedServices.TelegramBot.Interfaces
{
    public interface IAsyncStateMachine
    {
        IAsyncState CurrentState { get; }
        long UserId { get; }
        long ChatId { get; }
        MainContext MainContext { get; }

        Task ChangeStateAsync(IAsyncState newState);
        Task ProcessAsync(Message message);
        Task ProcessAsync(CallbackQuery query);
    }
}
