using SPOTService.Infrastructure.HostedServices.TelegramBot.AbstractClass;
using SPOTService.Infrastructure.HostedServices.TelegramBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SPOTService.Infrastructure.HostedServices.TelegramBot.States.Register
{
    /// <summary>
    /// Состояние отмены регистрации.
    /// </summary>
    public class CancelRegisterState() : AAsyncState, IAsyncState
    {
        /// <inheritdoc/>
        public Task EnterAsync(TelegramBotClient botClient, IAsyncStateMachine stateMachine)
        {
            _botClient = botClient;
            _stateMachine = stateMachine;
            _userId = _stateMachine.UserId;
            _chatId = _stateMachine.ChatId;
            _serviceScope = _stateMachine.ServiceScope;
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task ExecuteAsync(Message message)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task ExecuteAsync(CallbackQuery query)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task ExitAsync()
        {
            throw new NotImplementedException();
        }
    }
}
