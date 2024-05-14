using SPOTService.Dto.Questions;

namespace SPOTService.Dto.AnswerVariants
{
    /// <summary>
    /// Модель выходных даных варианта ответа
    /// </summary>
    public class AnswerVariantOutputDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Заголовок
        /// </summary>
        public required string Title { get; set; }
        public int QuestionId { get; set; }
        public virtual QuestionOutputDto Question { get; set; }
    }
}
