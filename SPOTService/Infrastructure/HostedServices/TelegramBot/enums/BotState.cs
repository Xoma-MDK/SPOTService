namespace SPOTService.Infrastructure.HostedServices.TelegramBot.enums
{
    public enum BotState
    {
        Idle = 0,
        WaitingForCode = 1,
        WaitingForRegisterResponent = 2,
        SurveyInProgress = 3,
        RegisterResponent = 4
    }
}
