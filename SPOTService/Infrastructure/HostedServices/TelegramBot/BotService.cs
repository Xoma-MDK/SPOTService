using SPOTService.DataStorage;
using SPOTService.Infrastructure.HostedServices.TelegramBot.Interfaces;
using SPOTService.Infrastructure.HostedServices.TelegramBot.States;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SPOTService.Infrastructure.HostedServices.TelegramBot
{
    /// <summary>
    /// Сервис фоновой задачи для управления Telegram-ботом.
    /// </summary>
    public class BotService(ILogger<BotService> _logger, IServiceProvider _serviceScope) : BackgroundService
    {
        private readonly TelegramBotClient _botClient = new("6667947721:AAE3M-U2KmCTzPzkE__5L3BIcQl-ZWHpYKE");
        private readonly Dictionary<long, IAsyncStateMachine> _userStateMachine = [];
        private readonly ReceiverOptions _receiverOptions = new()
        {
            AllowedUpdates =
        [
            UpdateType.Message,
            UpdateType.CallbackQuery
        ],
            ThrowPendingUpdates = true,
        };

        /// <summary>
        /// Основной метод, выполняющийся при запуске фоновой задачи.
        /// </summary>
        /// <param name="stoppingToken">Токен для отмены операции.</param>
        /// <returns>Задача, представляющая выполнение фоновой задачи.</returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await RestoreBotStateAsync();
            _botClient.StartReceiving(
            UpdateHandler,
            ErrorHandler,
            _receiverOptions,
            stoppingToken);
            var me = await _botClient.GetMeAsync(stoppingToken);
            _logger.LogInformation("{} запущен!", me.FirstName);
        }
        /// <summary>
        /// Восстанавливает состояние бота при запуске.
        /// </summary>
        /// <returns>Задача асинхронного выполнения восстановления состояния бота.</returns>
        private async Task RestoreBotStateAsync()
        {
            using var scope = _serviceScope.CreateScope();
            using var mainContext = scope.ServiceProvider.GetRequiredService<MainContext>();
            var respondents = mainContext.Respondents.ToList();
            foreach (var respondent in respondents)
            {
                if (!_userStateMachine.ContainsKey(respondent.TelegramId))
                {
                    _userStateMachine[respondent.TelegramId] = new AsyncStateMachine(respondent.TelegramId, respondent.TelegramChatId, _botClient, _serviceScope);
                    await _userStateMachine[respondent.TelegramId].RestoreStateAsync((int)respondent.StateId!);
                }
            }
        }
        /// <summary>
        /// Обработчик обновлений Telegram.
        /// </summary>
        /// <param name="botClient">Клиент Telegram бота.</param>
        /// <param name="update">Обновление от Telegram.</param>
        /// <param name="cancellationToken">Токен для отмены операции.</param>
        /// <returns>Задача асинхронного выполнения обработки обновления.</returns>
        private async Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            try
            {
                switch (update.Type)
                {
                    case UpdateType.Message:
                        {
                            await OnMessage(update.Message!);
                            break;
                        }
                    case UpdateType.CallbackQuery:
                        {
                            await OnCallbackQuery(update.CallbackQuery!);
                            break;
                        }

                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: {}", ex.Message);
            }
        }
        /// <summary>
        /// Обработчик ошибок, возникающих при работе с Telegram API.
        /// </summary>
        /// <param name="botClient">Клиент Telegram бота.</param>
        /// <param name="error">Исключение, представляющее ошибку.</param>
        /// <param name="cancellationToken">Токен для отмены операции.</param>
        /// <returns>Задача асинхронного выполнения обработки ошибки.</returns>
        private Task ErrorHandler(ITelegramBotClient botClient, Exception error, CancellationToken cancellationToken)
        {
            // Тут создадим переменную, в которую поместим код ошибки и её сообщение 
            var ErrorMessage = error switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => error.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
        /// <summary>
        /// Обработчик сообщений от пользователя.
        /// </summary>
        /// <param name="message">Сообщение от пользователя.</param>
        /// <returns>Задача асинхронного выполнения обработки сообщения.</returns>
        public async Task OnMessage(Message message)
        {

            if (message.Type == MessageType.Text)
            {
                var chatId = message.Chat.Id;
                var userId = message.From!.Id;

                if (!_userStateMachine.TryGetValue(userId, out IAsyncStateMachine? value))
                {
                    value = new AsyncStateMachine(userId, chatId, _botClient, _serviceScope);
                    _userStateMachine[userId] = value;
                    await _userStateMachine[userId].ChangeStateAsync(new IdleState());
                }
                await value.ProcessAsync(message);
            }
        }

        /// <summary>
        /// Обработчик колбэк-запросов от пользователя.
        /// </summary>
        /// <param name="callbackQuery">Колбэк-запрос от пользователя.</param>
        /// <returns>Задача асинхронного выполнения обработки колбэк-запроса.</returns>
        public async Task OnCallbackQuery(CallbackQuery callbackQuery)
        {
            var userId = callbackQuery.From.Id;

            await _userStateMachine[userId].ProcessAsync(callbackQuery);
        }
    }

}
