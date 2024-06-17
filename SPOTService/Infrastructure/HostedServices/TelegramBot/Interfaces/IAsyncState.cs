using Telegram.Bot;
using Telegram.Bot.Types;

namespace SPOTService.Infrastructure.HostedServices.TelegramBot.Interfaces
{
    /// <summary>
    /// Интерфейс для асинхронного состояния бота Telegram.
    /// </summary>
    public interface IAsyncState
    {
        /// <summary>
        /// Метод для входа в состояние.
        /// </summary>
        /// <param name="botClient">Клиент Telegram бота.</param>
        /// <param name="stateMachine">Машина состояний.</param>
        Task EnterAsync(TelegramBotClient botClient, IAsyncStateMachine stateMachine);

        /// <summary>
        /// Метод для выполнения действия на основе сообщения.
        /// </summary>
        /// <param name="message">Сообщение от пользователя.</param>
        Task ExecuteAsync(Message message);

        /// <summary>
        /// Метод для выполнения действия на основе коллбэк-запроса.
        /// </summary>
        /// <param name="query">Коллбэк-запрос от пользователя.</param>
        Task ExecuteAsync(CallbackQuery query);

        /// <summary>
        /// Метод для выхода из текущего состояния.
        /// </summary>
        Task ExitAsync();
    }
}
