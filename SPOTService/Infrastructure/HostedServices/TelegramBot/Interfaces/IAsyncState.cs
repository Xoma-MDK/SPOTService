using Telegram.Bot;
using Telegram.Bot.Types;

namespace SPOTService.Infrastructure.HostedServices.TelegramBot.Interfaces
{
    public interface IAsyncState
    {
        Task EnterAsync(TelegramBotClient botClient, IAsyncStateMachine stateMachine);
        Task ExecuteAsync(Message message);
        Task ExecuteAsync(CallbackQuery query);
        Task ExitAsync();
    }
}
