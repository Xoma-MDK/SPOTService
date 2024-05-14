using SPOTService.Dto.Respondent;

namespace SPOTService.Dto.Answers
{
    /// <summary>
    /// Модель выходных данных ответа
    /// </summary>
    public class AnswerOutputDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Идентификатор вопроса
        /// </summary>
        public int QuestionId { get; set; }
        /// <summary>
        /// Идентификатор опрашиваемого
        /// </summary>
        public int RespondentId { get; set; }
        /// <summary>
        /// Идентификатор варианта ответа
        /// </summary>
        public int AnswerVariantId { get; set; }
        /// <summary>
        /// Ответ на открытый вопрос
        /// </summary>
        public string? OpenAnswer { get; set; }
        /// <summary>
        /// Опрашмваемый
        /// </summary>
        public virtual RespondentOutputDto? Respondent { get; set; }
    }
}
