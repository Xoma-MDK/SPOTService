using SPOTService.DataStorage.Entities;
using SPOTService.Infrastructure.HostedServices.TelegramBot.enums;
using SPOTService.Infrastructure.HostedServices.TelegramBot.Interfaces;
using SPOTService.Infrastructure.HostedServices.TelegramBot.States.Register;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SPOTService.Infrastructure.HostedServices.TelegramBot.States
{
    public class IdleState() : IAsyncState
    {
        private TelegramBotClient _botClient;
        private IAsyncStateMachine _stateMachine;

        public async Task EnterAsync(TelegramBotClient botClient, IAsyncStateMachine stateMachine)
        {
            _botClient = botClient;
            _stateMachine = stateMachine;
        }

        public async Task ExecuteAsync(Message message)
        {
            if (message.Text == "/start")
            {
                await _botClient.SendTextMessageAsync(message.From!.Id, "Привет!");
                await _stateMachine.ChangeStateAsync(new WaitingForRegisterResponentState(_stateMachine.ServiceScope));
            }
        }

        public async Task ExecuteAsync(CallbackQuery query)
        {
            await _botClient.AnswerCallbackQueryAsync(query.Id);
        }

        public async Task ExitAsync()
        {
        }
    }
}
