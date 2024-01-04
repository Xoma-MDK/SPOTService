using SPOTService.Dto.Answers;
using SPOTService.Dto.Groups;
using SPOTService.Dto.Questions;
using SPOTService.Dto.User;
using System.Globalization;
using System.Text;

namespace SPOTService.Dto.Surveys
{
    public class SurveyInputDto
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required string AccessCode { get; set; }
        public DateTimeOffset? StartTime { get; set; }
        public DateTimeOffset? EndTime { get; set; }
        public bool Active { get; set; }
        public int GroupId { get; set; }
        public string? Department { get; set; }
        public int UserId { get; set; }
        public IEnumerable<QuestionIntputDto>? Questions { get; set; }
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
