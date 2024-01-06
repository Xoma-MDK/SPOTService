namespace SPOTService.Dto.AnswerVariants
{
    /// <summary>
    /// Модель обновления даных варианта ответа
    /// </summary>
    public class AnswerVariantUpdateDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Заголовок
        /// </summary>
        public required string Title { get; set; }
    }
}
