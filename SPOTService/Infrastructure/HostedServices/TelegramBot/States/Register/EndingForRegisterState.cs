using SPOTService.DataStorage.Entities;
using SPOTService.DataStorage;
using SPOTService.Infrastructure.HostedServices.TelegramBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using SPOTService.Infrastructure.HostedServices.TelegramBot.AbstractClass;
using SPOTService.Infrastructure.HostedServices.TelegramBot.States.Menu;

namespace SPOTService.Infrastructure.HostedServices.TelegramBot.States.Register
{
    public class EndingForRegisterState(IServiceProvider serviceScope) : AAsyncState, IAsyncState
    {
        public async Task EnterAsync(TelegramBotClient botClient, IAsyncStateMachine stateMachine)
        {
            _botClient = botClient;
            _stateMachine = stateMachine;
            _userId = _stateMachine.UserId;
            _chatId = _stateMachine.ChatId;
            _serviceScope = serviceScope;

            using var scope = _serviceScope.CreateScope();
            using var mainContext = scope.ServiceProvider.GetRequiredService<MainContext>();

            var groups = mainContext.Groups.ToList();
            var buttons = groups.Select(g => InlineKeyboardButton.WithCallbackData(g.Title, $"Group:{g.Id}"));
            var keyboard = new InlineKeyboardMarkup(buttons);
            await _botClient.SendTextMessageAsync(_chatId, "Выбери свою группу:", replyMarkup: keyboard);
        }

        public async Task ExecuteAsync(Message message)
        {
            using var scope = _serviceScope.CreateScope();
            using var mainContext = scope.ServiceProvider.GetRequiredService<MainContext>();
            var groups = mainContext.Groups.ToList();
            var buttons = groups.Select(g => InlineKeyboardButton.WithCallbackData(g.Title, $"Group:{g.Id}"));
            var keyboard = new InlineKeyboardMarkup(buttons);
            await _botClient.SendTextMessageAsync
                (
                _chatId,
                "Пожалуйста используй кнопки!\n" +
                "Выбери свою группу:",
                replyMarkup: keyboard
                );
        }

        public async Task ExecuteAsync(CallbackQuery query)
        {
            if (query.Data!.StartsWith("Group"))
            {
                using var scope = _serviceScope.CreateScope();
                using var mainContext = scope.ServiceProvider.GetRequiredService<MainContext>();
                var groupId = int.Parse(query.Data.Split(':')[1]);
                var respondent = mainContext.Respondents.FirstOrDefault(r => r.TelegramId == _userId);
                if (respondent != null)
                {
                    respondent.GroupId = groupId;
                    mainContext.Update(respondent);
                    await mainContext.SaveChangesAsync();
                }
                else
                {
                    respondent = new Respondent()
                    {
                        GroupId = groupId,
                        TelegramId = _userId,
                        TelegramChatId = _chatId
                    };
                    var respondentEntity = await mainContext.AddAsync(respondent);
                    await mainContext.SaveChangesAsync();
                }
                await _botClient.AnswerCallbackQueryAsync(query.Id);
                await _stateMachine.ChangeStateAsync(new MainMenuState(_stateMachine.ServiceScope));
            }
        }

        public async Task ExitAsync()
        {
        }
    }
}
