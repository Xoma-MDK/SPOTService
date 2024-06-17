using SPOTService.DataStorage;
using SPOTService.DataStorage.Entities;
using SPOTService.Infrastructure.HostedServices.TelegramBot.AbstractClass;
using SPOTService.Infrastructure.HostedServices.TelegramBot.Interfaces;
using System.Diagnostics;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SPOTService.Infrastructure.HostedServices.TelegramBot.States.Register
{
    /// <summary>
    /// Состояние ожидания ввода фамилии опрашиваемого.
    /// </summary>
    public class WaitingEnterSurnameState(IServiceProvider serviceScope) : AAsyncState, IAsyncState
    {
        /// <inheritdoc/>
        public async Task EnterAsync(TelegramBotClient botClient, IAsyncStateMachine stateMachine)
        {
            _botClient = botClient;
            _stateMachine = stateMachine;
            _userId = _stateMachine.UserId;
            _chatId = _stateMachine.ChatId;
            _serviceScope = serviceScope;
            await _botClient.SendTextMessageAsync(_chatId, "Хорошо, тогда напиши свою фамилию!");
        }

        /// <inheritdoc/>
        public async Task ExecuteAsync(Message message)
        {
            if (message.Text != "")
            {
                try
                {
                    using var scope = _serviceScope.CreateScope();
                    using var mainContext = scope.ServiceProvider.GetRequiredService<MainContext>();
                    var respondent = new Respondent()
                    {
                        Surname = message.Text,
                        TelegramId = _userId,
                        TelegramChatId = _chatId
                    };
                    await mainContext.AddAsync(respondent);
                    await mainContext.SaveChangesAsync();
                    await _stateMachine.ChangeStateAsync(new WaitingEnterNameState(_stateMachine.ServiceScope));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }


            }
            else
            {
                await _botClient.SendTextMessageAsync(_chatId, "Фамилия не может быть пустой!");
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
