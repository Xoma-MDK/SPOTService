using Telegram.Bot.Types;

namespace SPOTService.Infrastructure.HostedServices.TelegramBot.Interfaces
{
    /// <summary>
    /// Интерфейс для управления состоянием асинхронного состояния бота Telegram.
    /// </summary>
    public interface IAsyncStateMachine
    {
        /// <summary>
        /// Текущее состояние асинхронного автомата.
        /// </summary>
        IAsyncState? CurrentState { get; }

        /// <summary>
        /// Идентификатор пользователя Telegram.
        /// </summary>
        long UserId { get; }

        /// <summary>
        /// Идентификатор чата Telegram.
        /// </summary>
        long ChatId { get; }

        /// <summary>
        /// Область провайдера сервисов для доступа к зависимостям.
        /// </summary>
        IServiceProvider ServiceScope { get; }

        /// <summary>
        /// Метод для изменения текущего состояния.
        /// </summary>
        /// <param name="newState">Новое состояние, в которое необходимо перейти.</param>
        Task ChangeStateAsync(IAsyncState newState);

        /// <summary>
        /// Метод для восстановления состояния по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор состояния, которое необходимо восстановить.</param>
        Task RestoreStateAsync(int id);

        /// <summary>
        /// Обработка сообщения от пользователя.
        /// </summary>
        /// <param name="message">Сообщение от пользователя.</param>
        Task ProcessAsync(Message message);

        /// <summary>
        /// Обработка коллбэк-запроса от пользователя.
        /// </summary>
        /// <param name="query">Коллбэк-запрос от пользователя.</param>
        Task ProcessAsync(CallbackQuery query);
    }
}
