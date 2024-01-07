using SPOTService.Infrastructure.HostedServices.TelegramBot.Interfaces;
using SPOTService.Infrastructure.HostedServices.TelegramBot.States;

namespace SPOTService.Infrastructure.HostedServices.TelegramBot.enums
{
    public enum BotState
    {
        IdleState,
        WaitingForRegisterResponentState,
        RegisterResponentState,
        WaitingEnterSurnameState,
        WaitingEnterNameState,
        WaitingEnterPatronomycState,
        EndingForRegisterState, 
        MainMenuState,
        WaitingForCodeState,
        AskQuestionsState,
    }
}
