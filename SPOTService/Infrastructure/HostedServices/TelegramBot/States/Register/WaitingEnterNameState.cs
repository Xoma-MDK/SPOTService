﻿using SPOTService.DataStorage;
using SPOTService.Infrastructure.HostedServices.TelegramBot.AbstractClass;
using SPOTService.Infrastructure.HostedServices.TelegramBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SPOTService.Infrastructure.HostedServices.TelegramBot.States.Register
{
    public class WaitingEnterNameState(MainContext mainContext) : AAsyncState, IAsyncState
    {
        public async Task EnterAsync(TelegramBotClient botClient, IAsyncStateMachine stateMachine)
        {
            _botClient = botClient;
            _stateMachine = stateMachine;
            _userId = _stateMachine.UserId;
            _chatId = _stateMachine.ChatId;
            _mainContext = mainContext;
            await _botClient.SendTextMessageAsync(_chatId, "Отлично, теперь напиши свое имя!");
        }

        public async Task ExecuteAsync(Message message)
        {
            if (message.Text != "")
            {
                var respondent = _mainContext.Respondents.First(r => r.TelegramId == _userId);
                if (respondent != null)
                {
                    respondent.Name = message.Text;
                    _mainContext.Update(respondent);
                    await _mainContext.SaveChangesAsync();
                }
                await _stateMachine.ChangeStateAsync(new WaitingEnterPatronomycState(_stateMachine.MainContext));
            }
            else
            {
                await _botClient.SendTextMessageAsync(_chatId, "Имя не может быть пустым!");
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