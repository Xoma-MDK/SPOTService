using SPOTService.Dto.Answers;
using SPOTService.Dto.Groups;
using SPOTService.Dto.Questions;
using SPOTService.Dto.User;
using System.Globalization;
using System.Text;

namespace SPOTService.Dto.Surveys
{
    /// <summary>
    /// Модель входных данных опроса
    /// </summary>
    public class SurveyInputDto
    {
        /// <summary>
        /// Заголовок
        /// </summary>
        public required string Title { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Код доступа к опросу в Telegram боте
        /// </summary>
        public required string AccessCode { get; set; }
        /// <summary>
        /// Время начала опроса
        /// </summary>
        public DateTimeOffset? StartTime { get; set; }
        /// <summary>
        /// Время окончания опроса
        /// </summary>
        public DateTimeOffset? EndTime { get; set; }
        /// <summary>
        /// Флаг активности опроса
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// Идентификатор группы
        /// </summary>
        public int GroupId { get; set; }
        /// <summary>
        /// Отдел
        /// </summary>
        public string? Department { get; set; }
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Вопросы
        /// </summary>
        public IEnumerable<QuestionIntputDto>? Questions { get; set; }
        /// <summary>
        /// Перевести объект опроса в строку
        /// </summary>
        /// <returns>Объект опроса в строке</returns>
        public override string ToString()
        {
            return $"\nTitle: {Title}, \n" +
                   $"Description: {Description ?? "N/A"}, \n" +
                   $"AccessCode: {AccessCode}, \n" +
                   $"StartTime: {StartTime?.ToString(CultureInfo.InvariantCulture) ?? "N/A"}, \n" +
                   $"EndTime: {EndTime?.ToString(CultureInfo.InvariantCulture) ?? "N/A"}, \n" +
                   $"Active: {Active}, \n" +
                   $"GroupId: {GroupId}, \n" +
                   $"Department: {Department ?? "N/A"}, \n" +
                   $"UserId: {UserId}, \n" +
                   $"Questions: {QuestionsToString()}\n";
        }
        private string QuestionsToString()
        {
            if (Questions == null)
                return "N/A";

            return "\n[" + string.Join(", \n", Questions) + "]\n";
        }
    }
}
