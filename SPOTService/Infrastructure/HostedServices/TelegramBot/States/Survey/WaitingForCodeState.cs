using SPOTService.DataStorage;
using SPOTService.DataStorage.Entities;
using SPOTService.Infrastructure.HostedServices.TelegramBot.AbstractClass;
using SPOTService.Infrastructure.HostedServices.TelegramBot.Interfaces;
using SPOTService.Infrastructure.HostedServices.TelegramBot.States.Menu;
using SPOTService.Infrastructure.HostedServices.TelegramBot.States.Register;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace SPOTService.Infrastructure.HostedServices.TelegramBot.States.Survey
{
    public class WaitingForCodeState : AAsyncState, IAsyncState
    {
        public WaitingForCodeState(MainContext mainContext)
        {
            _mainContext = mainContext;
        }
        public WaitingForCodeState(MainContext mainContext, TelegramBotClient botClient, IAsyncStateMachine stateMachine)
        {
            _botClient = botClient;
            _stateMachine = stateMachine;
            _userId = _stateMachine.UserId;
            _chatId = _stateMachine.ChatId;
            _mainContext = mainContext;
        }
        public async Task EnterAsync(TelegramBotClient botClient, IAsyncStateMachine stateMachine)
        { 
            _botClient = botClient;
            _stateMachine = stateMachine;
            _userId = _stateMachine.UserId;
            _chatId = _stateMachine.ChatId;
            var replyKeyboard = new ReplyKeyboardMarkup
            (
                new List<KeyboardButton[]>()
                {
                        new KeyboardButton[]
                        {
                             new KeyboardButton("Отмена!"),
                        },
                }
            )
            {
                ResizeKeyboard = true
            };
            await _botClient.SendTextMessageAsync(_chatId,"Отправь мне код опроса.\n", replyMarkup: replyKeyboard);
        }

        public async Task ExecuteAsync(Message message)
        {
            if(message.Text != null && message.Text == "Отмена!")
            {
                await _stateMachine.ChangeStateAsync(new MainMenuState(_stateMachine.MainContext));
                return;
            }
            _mainContext.ChangeTracker.Clear();
            var survay = _mainContext.Surveys.Where(s => s.AccessCode == message.Text!.Trim()).FirstOrDefault();
            if (survay != null)
            {
                if (survay.Active && survay.EndTime > DateTime.UtcNow)
                {
                    await _botClient.SendTextMessageAsync(
                        message.From!.Id,
                        "Опрос найден!\n" +
                        $"{survay.Title}\n" +
                        $"{survay.Description}\n" +
                        $"Время и дата открытия: {survay.StartTime}\n" +
                        $"Время и дата закрытия: {survay.EndTime}\n" +
                        $"Для группы: {survay.Group!.Title}\n" +
                        $"Организатор: {survay.User!.Surname} {survay.User!.Name[0]}. {survay.User!.Patronomyc![0]}.");
                    await _stateMachine.ChangeStateAsync(new AskQuestionsState(_stateMachine.MainContext, survay));
                }
                else
                {
                    await _botClient.SendTextMessageAsync(_chatId, "Опрос найден, но в данный момент не доступен для прохожнения!");
                }

            }
            else
            {
                await _botClient.SendTextMessageAsync(_chatId, "Опроса для этого кода нету.\n" +
                    "Попробуй еще!");
            }
        }

        public async Task ExecuteAsync(CallbackQuery query)
        {
        }

        public async Task ExitAsync()
        {
        }
    }
}
