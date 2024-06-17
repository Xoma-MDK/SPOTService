using SPOTService.DataStorage;
using SPOTService.Infrastructure.HostedServices.TelegramBot.AbstractClass;
using SPOTService.Infrastructure.HostedServices.TelegramBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SPOTService.Infrastructure.HostedServices.TelegramBot.States.Register
{
    /// <summary>
    /// Состояние ожидания ввода имени опрашиваемого.
    /// </summary>
    public class WaitingEnterNameState(IServiceProvider serviceScope) : AAsyncState, IAsyncState
    {
        /// <inheritdoc/>
        public async Task EnterAsync(TelegramBotClient botClient, IAsyncStateMachine stateMachine)
        {
            _botClient = botClient;
            _stateMachine = stateMachine;
            _userId = _stateMachine.UserId;
            _chatId = _stateMachine.ChatId;
            _serviceScope = serviceScope;
            await _botClient.SendTextMessageAsync(_chatId, "Отлично, теперь напиши свое имя!");
        }

        /// <inheritdoc/>
        public async Task ExecuteAsync(Message message)
        {
            if (message.Text != "")
            {
                using var scope = _serviceScope.CreateScope();
                using var mainContext = scope.ServiceProvider.GetRequiredService<MainContext>();

                var respondent = mainContext!.Respondents.First(r => r.TelegramId == _userId);
                if (respondent != null)
                {
                    respondent.Name = message.Text;
                    mainContext.Update(respondent);
                    await mainContext.SaveChangesAsync();
                }
                await _stateMachine.ChangeStateAsync(new WaitingEnterPatronymicState(_stateMachine.ServiceScope));
            }
            else
            {
                await _botClient.SendTextMessageAsync(_chatId, "Имя не может быть пустым!");
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
