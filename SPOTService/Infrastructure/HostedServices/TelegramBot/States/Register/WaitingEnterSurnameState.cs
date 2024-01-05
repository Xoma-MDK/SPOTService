using SPOTService.DataStorage;
using SPOTService.DataStorage.Entities;
using SPOTService.Infrastructure.HostedServices.TelegramBot.AbstractClass;
using SPOTService.Infrastructure.HostedServices.TelegramBot.Interfaces;
using System.Diagnostics;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SPOTService.Infrastructure.HostedServices.TelegramBot.States.Register
{
    public class WaitingEnterSurnameState(MainContext mainContext) : AAsyncState, IAsyncState
    {
        public async Task EnterAsync(TelegramBotClient botClient, IAsyncStateMachine stateMachine)
        {
            _botClient = botClient;
            _stateMachine = stateMachine;
            _userId = _stateMachine.UserId;
            _chatId = _stateMachine.ChatId;
            _mainContext = mainContext;
            await _botClient.SendTextMessageAsync(_chatId, "Хорошо, тогда напиши свою фамилию!");
        }

        public async Task ExecuteAsync(Message message)
        {
            if (message.Text != "")
            {
                try
                {
                    var respondent = new Respondent()
                    {
                        Surname = message.Text,
                        TelegramId = _userId
                    };
                    await _mainContext.AddAsync(respondent);
                    await _mainContext.SaveChangesAsync();
                    await _stateMachine.ChangeStateAsync(new WaitingEnterNameState(_stateMachine.MainContext));
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

        public async Task ExecuteAsync(CallbackQuery query)
        {
            await _botClient.AnswerCallbackQueryAsync(query.Id);
        }

        public async Task ExitAsync()
        {

        }
    }
}
