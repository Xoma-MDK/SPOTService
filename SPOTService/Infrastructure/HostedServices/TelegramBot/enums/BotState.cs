namespace SPOTService.Infrastructure.HostedServices.TelegramBot.Enums
{
    /// <summary>
    /// Состояния бота Telegram.
    /// </summary>
    public enum BotState
    {
        /// <summary>
        /// Начальное состояние, когда бот ожидает действий пользователя.
        /// </summary>
        IdleState,

        /// <summary>
        /// Состояние ожидания регистрации опрашиваемого.
        /// </summary>
        WaitingForRegisterRespondentState,

        /// <summary>
        /// Состояние регистрации опрашиваемого.
        /// </summary>
        RegisterRespondentState,

        /// <summary>
        /// Состояние ожидания ввода фамилии опрашиваемого.
        /// </summary>
        WaitingEnterSurnameState,

        /// <summary>
        /// Состояние ожидания ввода имени опрашиваемого.
        /// </summary>
        WaitingEnterNameState,

        /// <summary>
        /// Состояние ожидания ввода отчества опрашиваемого.
        /// </summary>
        WaitingEnterPatronymicState,

        /// <summary>
        /// Завершение процесса регистрации опрашиваемого.
        /// </summary>
        EndingForRegisterState,

        /// <summary>
        /// Главное меню бота.
        /// </summary>
        MainMenuState,

        /// <summary>
        /// Состояние ожидания ввода кода опроса.
        /// </summary>
        WaitingForCodeState,

        /// <summary>
        /// Состояние задания вопросов в опросе.
        /// </summary>
        AskQuestionsState,
    }
}
