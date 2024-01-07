using System;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Polling;
using Telegram.Bot.Exceptions;
using SPOTService.DataStorage;
using SPOTService.DataStorage.Entities;
using SPOTService.Infrastructure.HostedServices.TelegramBot.enums;
using SPOTService.Infrastructure.HostedServices.TelegramBot.Interfaces;
using SPOTService.Infrastructure.HostedServices.TelegramBot.States;

namespace SPOTService.Infrastructure.HostedServices.TelegramBot
{
    public class BotService(ILogger<BotService> _logger, MainContext _mainContext) : BackgroundService
    {
        private readonly TelegramBotClient _botClient = new("6667947721:AAHvng4xyLEPrw42LIXDiuh0HHcoGHR3NIU");
        private readonly Dictionary<long, IAsyncStateMachine> _userStateMachine = new Dictionary<long, IAsyncStateMachine>();
        private readonly ReceiverOptions _receiverOptions = new()
        {
            AllowedUpdates =
        [
            UpdateType.Message,
            UpdateType.CallbackQuery
        ],
            ThrowPendingUpdates = true,
        };

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
        private async Task RestoreBotStateAsync()
        {
            var respondents = _mainContext.Respondents.ToList();
            foreach (var respondent in respondents)
            {
                if (!_userStateMachine.ContainsKey(respondent.TelegramId))
                {
                    _userStateMachine[respondent.TelegramId] = new AsyncStateMachine(respondent.TelegramId, respondent.TelegramChatId, _botClient, _mainContext);
                    await _userStateMachine[respondent.TelegramId].RestoreStateAsync((int)respondent.StateId!);
                }
            }
        }
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
        public async Task OnMessage(Message message)
        {

            if (message.Type == MessageType.Text)
            {
                var chatId = message.Chat.Id;
                var userId = message.From!.Id;
                
                if (!_userStateMachine.ContainsKey(userId)) 
                {
                    _userStateMachine[userId] = new AsyncStateMachine(userId, chatId, _botClient, _mainContext);
                    await _userStateMachine[userId].ChangeStateAsync(new IdleState());
                }
                await _userStateMachine[userId].ProcessAsync(message);
            }
        }

        public async Task OnCallbackQuery(CallbackQuery callbackQuery)
        {
            var userId = callbackQuery.From.Id;
            var chatId = callbackQuery.Message!.Chat.Id;

            await _userStateMachine[userId].ProcessAsync(callbackQuery);
        }
    }

}
