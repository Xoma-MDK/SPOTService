namespace SPOTService.Dto.Questions
{
    /// <summary>
    /// Модель входных данных вопроса
    /// </summary>
    public class QuestionInputDto
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
        public int QuestionGroupId { get; set; }
        public int SequenceNumber { get; set; }
        /// <summary>
        /// Перевести объект вопроса в строку
        /// </summary>
        /// <returns>Объект вопроса в строке</returns>
        public override string ToString()
        {
            return $"\nTitle: {Title},\n" +
                $" IsOpen: {IsOpen}, \n";
        }
    }
}
