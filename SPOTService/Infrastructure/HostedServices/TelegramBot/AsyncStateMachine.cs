using SPOTService.DataStorage;
using SPOTService.Infrastructure.HostedServices.TelegramBot.enums;
using SPOTService.Infrastructure.HostedServices.TelegramBot.Interfaces;
using System.Reflection;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SPOTService.Infrastructure.HostedServices.TelegramBot
{
    public class AsyncStateMachine(long userId, long chatId, TelegramBotClient botClient, IServiceProvider serviceScope) : IAsyncStateMachine
    {
        private IAsyncState _currentState;
        private long _userId = userId;
        private long _chatId = chatId;
        private TelegramBotClient _botClient = botClient;
        private IServiceProvider _serviceScope = serviceScope;

        public IAsyncState CurrentState
        {
            get { return _currentState; }
        }
        public long UserId
        {
            get { return _userId; }
        }
        public long ChatId
        {
            get { return _chatId; }
        }

        public IServiceProvider ServiceScope 
        { 
            get { return _serviceScope; } 
        }

        public async Task ChangeStateAsync(IAsyncState newState)
        {
            if (_currentState != null)
            {
                await _currentState.ExitAsync();
            }
            using var scope = _serviceScope.CreateScope();
            using var mainContext = scope.ServiceProvider.GetRequiredService<MainContext>();

            _currentState = newState;
            var respondent = mainContext.Respondents.Where(r => r.TelegramId == _userId).FirstOrDefault();
            if(respondent != null)
            {
                respondent.StateId = (int)Enum.Parse(typeof(BotState), newState.GetType().Name);
                await mainContext.SaveChangesAsync();
            }
            await _currentState.EnterAsync(_botClient, this);
        }
        public async Task RestoreStateAsync(int id)
        {
            var typeName = Enum.GetName(typeof(BotState), id);
            Type? type = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.Name == typeName);
            IAsyncState? instance = (IAsyncState)Activator.CreateInstance(type!, _serviceScope, _botClient, this)!;
            _currentState = instance;

        }

        public async Task ProcessAsync(Message message)
        {
            if (_currentState != null)
            {
                await _currentState.ExecuteAsync(message);
            }
        }

        public async Task ProcessAsync(CallbackQuery query)
        {
            if (_currentState != null)
            {
                await _currentState.ExecuteAsync(query);
            }
        }
    }
}
