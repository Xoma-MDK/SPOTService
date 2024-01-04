using SPOTService.Dto.AnswerVariants;

namespace SPOTService.Dto.Questions
{
    /// <summary>
    /// Модель выходных данных вопроса
    /// </summary>
    public class QuestionOutputDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Заголовок
        /// </summary>
        public required string Title { get; set; }
        /// <summary>
        /// Флаг "открытости" вопроса
        /// </summary>
        public bool IsOpen { get; set; }
        /// <summary>
        /// Варианты ответов
        /// </summary>
        public IEnumerable<AnswerVariantOutputDto>? AnswerVariants { get; set; }
    }
}
