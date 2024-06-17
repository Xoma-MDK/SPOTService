using SPOTService.DataStorage;
using SPOTService.Infrastructure.HostedServices.TelegramBot.Enums;
using SPOTService.Infrastructure.HostedServices.TelegramBot.Interfaces;
using System.Reflection;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SPOTService.Infrastructure.HostedServices.TelegramBot
{
    /// <summary>
    /// Асинхронная машина состояний для управления состояниями пользователей в Telegram боте.
    /// </summary>
    public class AsyncStateMachine(long userId, long chatId, TelegramBotClient botClient, IServiceProvider serviceScope) : IAsyncStateMachine
    {
        private IAsyncState? _currentState;

        /// <summary>
        /// Текущее состояние машины.
        /// </summary>
        public IAsyncState? CurrentState
        {
            get { return _currentState; }
        }
        /// <summary>
        /// Идентификатор пользователя Telegram.
        /// </summary>
        public long UserId
        {
            get { return userId; }
        }
        /// <summary>
        /// Идентификатор чата Telegram.
        /// </summary>
        public long ChatId
        {
            get { return chatId; }
        }

        /// <summary>
        /// Область провайдера сервисов.
        /// </summary>
        public IServiceProvider ServiceScope
        {
            get { return serviceScope; }
        }

        /// <inheritdoc/>
        public async Task ChangeStateAsync(IAsyncState newState)
        {
            if (_currentState != null)
            {
                await _currentState.ExitAsync();
            }
            using var scope = serviceScope.CreateScope();
            using var mainContext = scope.ServiceProvider.GetRequiredService<MainContext>();

            _currentState = newState;
            var respondent = mainContext.Respondents.Where(r => r.TelegramId == userId).FirstOrDefault();
            if (respondent != null)
            {
                respondent.StateId = (int)Enum.Parse(typeof(BotState), newState.GetType().Name);
                await mainContext.SaveChangesAsync();
            }
            await _currentState.EnterAsync(botClient, this);
        }

        /// <inheritdoc/>
        public Task RestoreStateAsync(int id)
        {
            var typeName = Enum.GetName(typeof(BotState), id);
            Type? type = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.Name == typeName);
            IAsyncState? instance = (IAsyncState)Activator.CreateInstance(type!, serviceScope, botClient, this)!;
            _currentState = instance;
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public async Task ProcessAsync(Message message)
        {
            if (_currentState != null)
            {
                await _currentState.ExecuteAsync(message);
            }
        }

        /// <inheritdoc/>
        public async Task ProcessAsync(CallbackQuery query)
        {
            if (_currentState != null)
            {
                await _currentState.ExecuteAsync(query);
            }
        }
    }
}
