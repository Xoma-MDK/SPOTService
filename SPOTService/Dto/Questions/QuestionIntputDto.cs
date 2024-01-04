using SPOTService.Dto.AnswerVariants;

namespace SPOTService.Dto.Questions
{
    /// <summary>
    /// Модель входных данных вопроса
    /// </summary>
    public class QuestionIntputDto
    {
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
        public IEnumerable<AnswerVariantInputDto>? AnswerVariants { get; set; }
        /// <summary>
        /// Перевести объект вопроса в строку
        /// </summary>
        /// <returns>Объект вопроса в строке</returns>
        public override string ToString()
        {
            return $"\nTitle: {Title},\n" +
                $" IsOpen: {IsOpen}, \n" +
                $"AnswerVariants: {AnswerVariantsToString()}\n";
        }
        private string AnswerVariantsToString()
        {
            if (AnswerVariants == null)
                return "N/A";

            return "\n[" + string.Join(", \n", AnswerVariants.Select(a => a.ToString())) + "]\n";
        }
    }
}
