using SPOTService.DataStorage;
using SPOTService.Infrastructure.HostedServices.TelegramBot.AbstractClass;
using SPOTService.Infrastructure.HostedServices.TelegramBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace SPOTService.Infrastructure.HostedServices.TelegramBot.States.Register
{
    /// <summary>
    /// Состояние ожидания ввода отчества опрашиваемого.
    /// </summary>
    public class WaitingEnterPatronymicState(IServiceProvider serviceScope) : AAsyncState, IAsyncState
    {
        /// <inheritdoc/>
        public async Task EnterAsync(TelegramBotClient botClient, IAsyncStateMachine stateMachine)
        {
            _botClient = botClient;
            _stateMachine = stateMachine;
            _userId = _stateMachine.UserId;
            _chatId = _stateMachine.ChatId;
            _serviceScope = serviceScope;
            var inlineKeyboard = new InlineKeyboardMarkup
            (
                new List<InlineKeyboardButton[]>()
                    {
                        new InlineKeyboardButton[]
                        {
                             InlineKeyboardButton.WithCallbackData("У меня нет отчества", "NonePatronymic"),
                        },
                    }
            );
            await _botClient.SendTextMessageAsync(_chatId, "Записал!\n" +
                "Осталось отчество, если оно есть у тебя, то напиши мне.\n" +
                "Если его нету, то нажми кнопку ниже.",
                replyMarkup: inlineKeyboard);
        }

        /// <inheritdoc/>
        public async Task ExecuteAsync(Message message)
        {
            if (message.Text != "")
            {
                using var scope = _serviceScope.CreateScope();
                using var mainContext = scope.ServiceProvider.GetRequiredService<MainContext>();

                var respondent = mainContext.Respondents.First(r => r.TelegramId == _userId);
                if (respondent != null)
                {
                    respondent.Patronymic = message.Text;
                    mainContext.Update(respondent);
                    await mainContext.SaveChangesAsync();
                }
                await _stateMachine.ChangeStateAsync(new EndingForRegisterState(_stateMachine.ServiceScope));
            }
            else
            {
                await _botClient.SendTextMessageAsync(_chatId, "Фамилия не может быть пустой!");
            }
        }

        /// <inheritdoc/>
        public async Task ExecuteAsync(CallbackQuery query)
        {
            if (query.Data == "NonePatronymic")
            {
                await _stateMachine.ChangeStateAsync(new EndingForRegisterState(_stateMachine.ServiceScope));
            }
        }

        /// <inheritdoc/>
        public Task ExitAsync()
        {
            return Task.CompletedTask;
        }
    }
}
