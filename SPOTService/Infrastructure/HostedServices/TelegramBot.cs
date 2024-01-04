using Telegram.Bot.Polling;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;

namespace SPOTService.Infrastructure.HostedServices
{
    public class TelegramBot(ILogger<TelegramBot> logger) : BackgroundService
    {
        private readonly ITelegramBotClient _botClient = new TelegramBotClient("6667947721:AAHvng4xyLEPrw42LIXDiuh0HHcoGHR3NIU");

        private readonly ReceiverOptions _receiverOptions = new()
        {
            AllowedUpdates =
                [
                UpdateType.Message,
                ],
            ThrowPendingUpdates = true,
        };

        private readonly ILogger<TelegramBot> _logger = logger;


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Main(stoppingToken);
        }

        private async Task Main(CancellationToken stoppingToken)
        {
            _botClient.StartReceiving(
                UpdateHandler, 
                ErrorHandler, 
                _receiverOptions, 
                stoppingToken); 
            var me = await _botClient.GetMeAsync(stoppingToken); 
            _logger.LogInformation("{} запущен!", me.FirstName);
        }
        private async Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            try
            {
                switch (update.Type)
                {
                    case UpdateType.Message:
                        {
                            var message = update.Message;
                            var user = message!.From;
                            _logger.LogInformation("{} ({}) написал сообщение: {}", user!.FirstName, user.Id, message.Text);
                            var chat = message.Chat;
                            await botClient.SendTextMessageAsync(
                                chat.Id, 
                                message.Text!, 
                                replyToMessageId: message.MessageId, // по желанию можем поставить этот параметр, отвечающий за "ответ" на сообщение
                                cancellationToken: cancellationToken);

                            return;
                        }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: {}", ex.Message);
            }
        }

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


    }
}
