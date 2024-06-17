using Microsoft.EntityFrameworkCore;
using SPOTService.DataStorage;
using SPOTService.Infrastructure.HostedServices.TelegramBot.AbstractClass;
using SPOTService.Infrastructure.HostedServices.TelegramBot.Interfaces;
using SPOTService.Infrastructure.HostedServices.TelegramBot.States.Menu;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace SPOTService.Infrastructure.HostedServices.TelegramBot.States.Survey
{
    /// <summary>
    /// Состояние ожидания ввода кода опроса.
    /// </summary>
    public class WaitingForCodeState : AAsyncState, IAsyncState
    {
        /// <summary>
        /// Конструктор состояния ожидания ввода кода опроса.
        /// </summary>
        /// <param name="serviceScope">Сервисный провайдер для доступа к зависимостям.</param>
        public WaitingForCodeState(IServiceProvider serviceScope)
        {
            _serviceScope = serviceScope;
        }
        /// <summary>
        /// Конструктор состояния ожидания ввода кода опроса.
        /// </summary>
        /// <param name="serviceScope">Сервисный провайдер для доступа к зависимостям.</param>
        /// <param name="botClient">Клиент Telegram Bot API.</param>
        /// <param name="stateMachine">Машина состояний для управления состояниями.</param>
        public WaitingForCodeState(IServiceProvider serviceScope, TelegramBotClient botClient, IAsyncStateMachine stateMachine)
        {
            _botClient = botClient;
            _stateMachine = stateMachine;
            _userId = _stateMachine.UserId;
            _chatId = _stateMachine.ChatId;
            _serviceScope = serviceScope;
        }

        /// <inheritdoc/>
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
                             new("Отмена!"),
                        },
                }
            )
            {
                ResizeKeyboard = true
            };
            await _botClient.SendTextMessageAsync(_chatId, "Отправь мне код опроса.\n", replyMarkup: replyKeyboard);
        }

        /// <inheritdoc/>
        public async Task ExecuteAsync(Message message)
        {
            if (message.Text != null && message.Text == "Отмена!")
            {
                await _stateMachine.ChangeStateAsync(new MainMenuState(_stateMachine.ServiceScope));
                return;
            }
            using var scope = _serviceScope.CreateScope();
            using var mainContext = scope.ServiceProvider.GetRequiredService<MainContext>();
            var survey = mainContext.Surveys.Include(q => q.Questions).Where(s => s.AccessCode == message.Text!.Trim()).FirstOrDefault();
            if (survey != null)
            {
                if (survey.Active && survey.EndTime > DateTime.UtcNow)
                {
                    await _botClient.SendTextMessageAsync(
                        message.From!.Id,
                        "Опрос найден!\n" +
                        $"{survey.Title}\n" +
                        $"{survey.Description}\n" +
                        $"Время и дата открытия: {survey.StartTime: HH:mm dd.MM.yyyy}\n" +
                        $"Время и дата закрытия: {survey.EndTime: HH:mm dd.MM.yyyy}\n" +
                        $"Для группы: {survey.Group!.Title}\n" +
                        $"Организатор: {survey.User!.Surname} {survey.User!.Name[0]}. {survey.User!.Patronomyc![0]}.", replyMarkup: new ReplyKeyboardRemove());
                    await _stateMachine.ChangeStateAsync(new AskQuestionsState(_stateMachine.ServiceScope, survey));
                }
                else
                {
                    await _botClient.SendTextMessageAsync(_chatId, "Опрос найден, но в данный момент не доступен для прохождения!");
                }

            }
            else
            {
                await _botClient.SendTextMessageAsync(_chatId, "Опроса для этого кода нету.\n" +
                    "Попробуй еще!");
            }
        }

        /// <inheritdoc/>
        public Task ExecuteAsync(CallbackQuery query)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task ExitAsync()
        {
            return Task.CompletedTask;
        }
    }
}
