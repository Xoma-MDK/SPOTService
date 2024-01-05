using System;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Polling;
using Telegram.Bot.Exceptions;
using SPOTService.DataStorage;
using SPOTService.DataStorage.Entities;
using SPOTService.Infrastructure.HostedServices.TelegramBot.enums;
using SPOTService.Infrastructure.HostedServices.TelegramBot.Interfaces;
using SPOTService.Infrastructure.HostedServices.TelegramBot.States;

namespace SPOTService.Infrastructure.HostedServices.TelegramBot
{
    public class BotService(ILogger<BotService> logger, MainContext mainContext) : BackgroundService
    {
        private readonly TelegramBotClient _botClient = new("6667947721:AAHvng4xyLEPrw42LIXDiuh0HHcoGHR3NIU");
        private readonly Dictionary<long, BotState> _userStates = new Dictionary<long, BotState>();
        private readonly Dictionary<long, IAsyncStateMachine> _userStateMachine = new Dictionary<long, IAsyncStateMachine>();
        private readonly ILogger<BotService> _logger = logger;
        private readonly ReceiverOptions _receiverOptions = new()
        {
            AllowedUpdates =
        [
            UpdateType.Message,
            UpdateType.CallbackQuery
        ],
            ThrowPendingUpdates = true,
        };

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _botClient.StartReceiving(
            UpdateHandler,
            ErrorHandler,
            _receiverOptions,
            stoppingToken);
            var me = await _botClient.GetMeAsync(stoppingToken);
            _logger.LogInformation("{} запущен!", me.FirstName);
        }
        private async Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            try
            {
                switch (update.Type)
                {
                    case UpdateType.Message:
                        {
                            await OnMessage(update.Message!);
                            break;
                        }
                    case UpdateType.CallbackQuery:
                        {
                            await OnCallbackQuery(update.CallbackQuery!);
                            break;
                        }

                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: {}", ex.Message);
            }
        }
        private Task ErrorHandler(ITelegramBotClient botClient, Exception error, CancellationToken cancellationToken)
        {
            // Тут создадим переменную, в которую поместим код ошибки и её сообщение 
            var ErrorMessage = error switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => error.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
        public async Task OnMessage(Message message)
        {

            if (message.Type == MessageType.Text)
            {
                var chatId = message.Chat.Id;
                var userId = message.From!.Id;
                
                if (!_userStateMachine.ContainsKey(userId)) 
                {
                    _userStateMachine[userId] = new AsyncStateMachine(userId, chatId, _botClient, mainContext);
                    await _userStateMachine[userId].ChangeStateAsync(new IdleState());
                }
                await _userStateMachine[userId].ProcessAsync(message);
                /*
                switch (_userStates.GetValueOrDefault(userId))
                {
                    case BotState.Idle:
                        {
                            if (message.Text == "/enter_code")
                            {
                                _userStates[userId] = BotState.WaitingForCode;
                                await _botClient.SendTextMessageAsync(chatId, "Отправьте код опроса");
                            }
                            break;
                        }
                    case BotState.WaitingForCode:
                        if (message.Text == "2eeqw")
                        {
                            if (mainContext.Respondents.Any(x => x.TelegramId == userId))
                            {
                                _userStates[userId] = BotState.SurveyInProgress;
                                await _botClient.SendTextMessageAsync(chatId, "Опрос начат. Ответьте на вопросы.");
                                await AskQuestions(chatId, userId);
                            }
                            else
                            {
                                _userStates[userId] = BotState.RegisterResponent;
                                var inlineKeyboard = new InlineKeyboardMarkup(
                                   new List<InlineKeyboardButton[]>()
                                   {
                                        new InlineKeyboardButton[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("Зарегистрироваться!", "reg"),
                                            InlineKeyboardButton.WithCallbackData("Анонимно!", "anon"),
                                        },
                                   });
                                await _botClient.SendTextMessageAsync(
                                    chatId,
                                    "Хотите ли вы зарегистрироваться?\n" +
                                    "Или желаете пройти опрос анонимно?",
                                    replyMarkup: inlineKeyboard);
                            }
                        }
                        else
                        {
                            await _botClient.SendTextMessageAsync(chatId, "Введите код для начала опроса.");
                        }
                        break;
                    case BotState.SurveyInProgress:
                        //await ProcessSurveyResponse(chatId, userId, message.Text!);
                        break;
                }*/
            }
        }

        public async Task OnCallbackQuery(CallbackQuery callbackQuery)
        {
            var userId = callbackQuery.From.Id;
            var chatId = callbackQuery.Message!.Chat.Id;

            await _userStateMachine[userId].ProcessAsync(callbackQuery);
            /*
            switch (_userStates.GetValueOrDefault(userId))
            {
                case BotState.RegisterResponent:
                    {
                        switch (callbackQuery.Data)
                        {
                            case "reg":
                                {
                                    break;
                                }
                            case "anon":
                                {
                                    _userStates[userId] = BotState.WaitingForRegisterResponent;
                                    var groups = mainContext.Groups.ToList();
                                    var buttons = groups.Select(g => InlineKeyboardButton.WithCallbackData(g.Title, $"Group:{g.Id}"));
                                    var keyboard = new InlineKeyboardMarkup(buttons);
                                    await _botClient.SendTextMessageAsync(chatId, "Выбери свою группу:", replyMarkup: keyboard);
                                    await _botClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                                    break;
                                }
                        }
                        break;
                    }
                case BotState.WaitingForRegisterResponent:
                    {
                        if (callbackQuery.Data!.StartsWith("Group"))
                        {
                            var groupId = int.Parse(callbackQuery.Data.Split(':')[1]);
                            var respondent = new Respondent()
                            {
                                GroupId = groupId,
                                TelegramId = userId
                            };
                            var respondentEntity = mainContext.Add(respondent);
                            await mainContext.SaveChangesAsync();
                            await _botClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                            _userStates[userId] = BotState.WaitingForCode;
                        }
                        break;
                    }
                case BotState.SurveyInProgress:
                    //await ProcessSurveyResponse(chatId, userId, callbackQuery.Data!);
                    break;
            }*/
        }



        private async Task ProcessSurveyResponse(long chatId, long userId, string answer)
        {
            var userResponse = new Answer
            {

            };

            mainContext.Add(userResponse);
            await mainContext.SaveChangesAsync();

            // Здесь можно отправить следующий вопрос или закончить опрос
            // Пример: await _botClient.SendTextMessageAsync(chatId, "Следующий вопрос?");

            // Если опрос завершен, вернем бота в состояние ожидания кода
            _userStates[userId] = BotState.Idle;
        }

        private async Task AskQuestions(long chatId, long userId)
        {

            var questions = mainContext.Surveys.FirstOrDefault(s => s.Id == 3)!.Questions!.ToList(); // Замените на ваш способ получения вопросов из базы данных

            foreach (var question in questions)
            {
                await _botClient.SendTextMessageAsync(chatId, question.Title);

                if (question.IsOpen == false)
                {
                    var options = question.AnswerVariants!.ToList();
                    var buttons = options.Select(o => InlineKeyboardButton.WithCallbackData(o.Title, o.Id.ToString()));
                    var keyboard = new InlineKeyboardMarkup(buttons);
                    await _botClient.SendTextMessageAsync(chatId, "Выберите ответ:", replyMarkup: keyboard);
                }
            }
            _userStates[userId] = BotState.Idle;
        }
    }

}
