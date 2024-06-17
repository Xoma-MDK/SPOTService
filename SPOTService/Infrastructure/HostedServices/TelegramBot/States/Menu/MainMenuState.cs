using SPOTService.Infrastructure.HostedServices.TelegramBot.AbstractClass;
using SPOTService.Infrastructure.HostedServices.TelegramBot.Interfaces;
using SPOTService.Infrastructure.HostedServices.TelegramBot.States.Survey;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace SPOTService.Infrastructure.HostedServices.TelegramBot.States.Menu
{
    /// <summary>
    /// Состояние главного меню бота.
    /// </summary>
    public class MainMenuState : AAsyncState, IAsyncState
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса MainMenuState.
        /// </summary>
        /// <param name="serviceScope">Провайдер служб для доступа к зависимостям.</param>
        public MainMenuState(IServiceProvider serviceScope)
        {
            _serviceScope = serviceScope;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса MainMenuState с передачей клиента Telegram и текущего состояния.
        /// </summary>
        /// <param name="serviceScope">Провайдер служб для доступа к зависимостям.</param>
        /// <param name="botClient">Клиент Telegram Bot API.</param>
        /// <param name="stateMachine">Асинхронный конечный автомат.</param>
        public MainMenuState(IServiceProvider serviceScope, TelegramBotClient botClient, IAsyncStateMachine stateMachine)
        {
            _botClient = botClient;
            _stateMachine = stateMachine;
            _userId = _stateMachine.UserId;
            _chatId = _stateMachine.ChatId;
            _serviceScope = serviceScope;
        }

        /// <inheritdoc/>
        public async Task EnterAsync(TelegramBotClient botClient, IAsyncStateMachine stateMachine)
        {
            _botClient = botClient;
            _stateMachine = stateMachine;
            _userId = _stateMachine.UserId;
            _chatId = _stateMachine.ChatId;
            var replyKeyboard = new ReplyKeyboardMarkup
            (
                new List<KeyboardButton[]>()
                {
                        new KeyboardButton[]
                        {
                             new("Пройти опрос"),
                        },
                }
            )
            {
                ResizeKeyboard = true
            };
            await _botClient.SendTextMessageAsync(
                _chatId,
                "Добро пожаловать!\n" +
                "Основное меню находиться в кнопках",
                replyMarkup: replyKeyboard);
        }

        /// <inheritdoc/>
        public async Task ExecuteAsync(Message message)
        {
            if (message.Text == "Пройти опрос")
            {
                await _stateMachine.ChangeStateAsync(new WaitingForCodeState(_stateMachine.ServiceScope));
            }
        }

        /// <inheritdoc/>
        public async Task ExecuteAsync(CallbackQuery query)
        {
            await _botClient.AnswerCallbackQueryAsync(query.Id);
        }

        /// <inheritdoc/>
        public Task ExitAsync()
        {
            return Task.CompletedTask;
        }
    }
}
