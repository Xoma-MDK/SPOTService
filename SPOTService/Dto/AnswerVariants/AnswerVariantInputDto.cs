
namespace SPOTService.Dto.AnswerVariants
{
    /// <summary>
    /// Модель входных данных варианта ответа
    /// </summary>
    public class AnswerVariantInputDto
    {
        /// <summary>
        /// Заголовок
        /// </summary>
        public required string Title { get; set; }
        public int QuestionId { get; set; }
        /// <summary>
        /// Перевести объект варианта ответа в строку
        /// </summary>
        /// <returns>Объект варианта ответа в строке</returns>
        public override string ToString()
        {
            return $"Title: {Title}";
        }
    }
}
