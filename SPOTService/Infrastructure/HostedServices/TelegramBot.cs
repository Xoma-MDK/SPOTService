using Telegram.Bot.Polling;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;

namespace SPOTService.Infrastructure.HostedServices
{
    public class TelegramBot : BackgroundService
    {
        // Это клиент для работы с Telegram Bot API, который позволяет отправлять сообщения, управлять ботом, подписываться на обновления и многое другое.
        private readonly ITelegramBotClient _botClient;

        // Это объект с настройками работы бота. Здесь мы будем указывать, какие типы Update мы будем получать, Timeout бота и так далее.
        private readonly ReceiverOptions _receiverOptions;

        private readonly ILogger<TelegramBot> _logger;

        public TelegramBot(ILogger<TelegramBot> logger)
        {
            _logger = logger;
            _botClient = new TelegramBotClient("6667947721:AAHvng4xyLEPrw42LIXDiuh0HHcoGHR3NIU"); // Присваиваем нашей переменной значение, в параметре передаем Token, полученный от BotFather
            _receiverOptions = new ReceiverOptions // Также присваем значение настройкам бота
            {
                AllowedUpdates =
                // Тут указываем типы получаемых Update`ов, о них подробнее расказано тут https://core.telegram.org/bots/api#update
                [
                UpdateType.Message, // Сообщения (текст, фото/видео, голосовые/видео сообщения и т.д.)
            ],
                // Параметр, отвечающий за обработку сообщений, пришедших за то время, когда ваш бот был оффлайн
                // True - не обрабатывать, False (стоит по умолчанию) - обрабаывать
                ThrowPendingUpdates = true,
            };
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Main();
        }

        private async Task Main()
        {

            using var cts = new CancellationTokenSource();

            // UpdateHander - обработчик приходящих Update`ов
            // ErrorHandler - обработчик ошибок, связанных с Bot API
            _botClient.StartReceiving(UpdateHandler, ErrorHandler, _receiverOptions, cts.Token); // Запускаем бота

            var me = await _botClient.GetMeAsync(); // Создаем переменную, в которую помещаем информацию о нашем боте.
            _logger.LogInformation("{} запущен!", me.FirstName);

            //await Task.Delay(-1); // Устанавливаем бесконечную задержку, чтобы наш бот работал постоянно
        }
        private async Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Обязательно ставим блок try-catch, чтобы наш бот не "падал" в случае каких-либо ошибок
            try
            {
                // Сразу же ставим конструкцию switch, чтобы обрабатывать приходящие Update
                switch (update.Type)
                {
                    case UpdateType.Message:
                        {
                            // эта переменная будет содержать в себе все связанное с сообщениями
                            var message = update.Message;

                            // From - это от кого пришло сообщение (или любой другой Update)
                            var user = message!.From;

                            // Выводим на экран то, что пишут нашему боту, а также небольшую информацию об отправителе
                            _logger.LogInformation("{} ({}) написал сообщение: {}", user!.FirstName, user.Id, message.Text);

                            // Chat - содержит всю информацию о чате
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
