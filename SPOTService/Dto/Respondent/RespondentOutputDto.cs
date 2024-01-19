using SPOTService.Dto.Groups;

namespace SPOTService.Dto.Respondent
{
    /// <summary>
    /// Модель выходных данных опрашиваемого
    /// </summary>
    public class RespondentOutputDto
    {
        /// <summary>
        /// Индетификатор
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string? Surname { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public string? Patronomic { get; set; }
        /// <summary>
        /// Индетификатор группы
        /// </summary>
        public int? GroupId { get; set; }
        /// <summary>
        /// Группа
        /// </summary>
        public GroupOutputDto? Group {  get; set; }
    }
}
