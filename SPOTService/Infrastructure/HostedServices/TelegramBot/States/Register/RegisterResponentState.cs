using SPOTService.DataStorage;
using SPOTService.DataStorage.Entities;
using SPOTService.Infrastructure.HostedServices.TelegramBot.AbstractClass;
using SPOTService.Infrastructure.HostedServices.TelegramBot.enums;
using SPOTService.Infrastructure.HostedServices.TelegramBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace SPOTService.Infrastructure.HostedServices.TelegramBot.States.Register
{
    public class RegisterResponentState(IServiceProvider serviceScope) : AAsyncState, IAsyncState
    {
        public async Task EnterAsync(TelegramBotClient botClient, IAsyncStateMachine stateMachine)
        {
            _botClient = botClient;
            _stateMachine = stateMachine;
            _userId = _stateMachine.UserId;
            _chatId = _stateMachine.ChatId;
            _serviceScope = serviceScope;
        }

        public async Task ExecuteAsync(Message message)
        {
            var inlineKeyboard = new InlineKeyboardMarkup
                (
                new List<InlineKeyboardButton[]>()
                    {
                        new InlineKeyboardButton[]
                        {
                             InlineKeyboardButton.WithCallbackData("Зарегистрироваться!", "reg"),
                             InlineKeyboardButton.WithCallbackData("Анонимно!", "anon"),
                        },
                    }
                );
            await _botClient.SendTextMessageAsync(
                _chatId,
                "Пожалуйста используй кнопки!\n" +
                "Хотите ли вы зарегистрироваться?\n" +
                "Или желаете пройти опрос анонимно?",
                replyMarkup: inlineKeyboard);
        }

        public async Task ExecuteAsync(CallbackQuery query)
        {
            switch (query.Data)
            {
                case "reg":
                    {
                        await _stateMachine.ChangeStateAsync(new WaitingEnterSurnameState(_stateMachine.ServiceScope));
                        await _botClient.AnswerCallbackQueryAsync(query.Id);
                        break;
                    }
                case "anon":
                    {
                        await _stateMachine.ChangeStateAsync(new EndingForRegisterState(_stateMachine.ServiceScope));
                        await _botClient.AnswerCallbackQueryAsync(query.Id);
                        break;
                    }
            }
        }

        public async Task ExitAsync()
        {
        }
    }
}
