namespace SPOTService.Dto.Roles
{
    /// <summary>
    /// Модель выходных данных роли
    /// </summary>
    public class RoleOutputDto
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
