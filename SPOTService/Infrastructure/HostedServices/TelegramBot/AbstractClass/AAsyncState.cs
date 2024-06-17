using SPOTService.Infrastructure.HostedServices.TelegramBot.Interfaces;
using Telegram.Bot;

namespace SPOTService.Infrastructure.HostedServices.TelegramBot.AbstractClass
{
    /// <summary>
    /// Абстрактный класс, представляющий базовое состояние для асинхронного обработчика в Telegram боте.
    /// </summary>
    public class AAsyncState
    {
        /// <summary>
        /// Клиент Telegram бота.
        /// </summary>
        protected TelegramBotClient _botClient;

        /// <summary>
        /// Состояние машины состояний для асинхронной обработки.
        /// </summary>
        protected IAsyncStateMachine _stateMachine;

        /// <summary>
        /// Область провайдера сервисов для разрешения зависимостей.
        /// </summary>
        protected IServiceProvider _serviceScope;

        /// <summary>
        /// Идентификатор пользователя Telegram.
        /// </summary>
        protected long _userId;

        /// <summary>
        /// Идентификатор чата Telegram.
        /// </summary>
        protected long _chatId;
    }

}
