using SPOTService.Infrastructure.HostedServices.TelegramBot.Interfaces;
using SPOTService.Infrastructure.HostedServices.TelegramBot.States.Register;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SPOTService.Infrastructure.HostedServices.TelegramBot.States
{

    /// <summary>
    /// Состояние ожидания начального взаимодействия с пользователем.
    /// </summary>
    public class IdleState() : IAsyncState
    {
        private TelegramBotClient? _botClient;
        private IAsyncStateMachine? _stateMachine;

        /// <inheritdoc/>
        public Task EnterAsync(TelegramBotClient botClient, IAsyncStateMachine stateMachine)
        {
            _botClient = botClient;
            _stateMachine = stateMachine;
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public async Task ExecuteAsync(Message message)
        {
            if (message.Text == "/start")
            {
                await _botClient!.SendTextMessageAsync(message.From!.Id, "Привет!");
                await _stateMachine!.ChangeStateAsync(new WaitingForRegisterRespondentState(_stateMachine.ServiceScope));
            }
        }
        /// <inheritdoc/>
        public async Task ExecuteAsync(CallbackQuery query)
        {
            await _botClient!.AnswerCallbackQueryAsync(query.Id);
        }
        /// <inheritdoc/>
        public Task ExitAsync()
        {
            return Task.CompletedTask;
        }
    }
}
