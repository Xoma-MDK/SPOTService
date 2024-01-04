namespace SPOTService.Dto.Groups
{
    /// <summary>
    /// Модель выходных данных группы
    /// </summary>
    public class GroupOutputDto
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
