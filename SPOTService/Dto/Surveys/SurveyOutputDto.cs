using SPOTService.Dto.Answers;
using SPOTService.Dto.Groups;
using SPOTService.Dto.Questions;
using SPOTService.Dto.User;

namespace SPOTService.Dto.Surveys
{
    public class SurveyOutputDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required string AccessCode { get; set; }
        public DateTimeOffset? StartTime { get; set; }
        public DateTimeOffset? EndTime { get; set; }
        public bool Active { get; set; }
        public int GroupId { get; set; }
        public string? Department { get; set; }
        public int UserId { get; set; }
        public GroupOutputDto? Group { get; set; }
        public UserOutputDto? User { get; set; }
        public IEnumerable<AnswerOutputDto>? Answers { get; set; }
        public IEnumerable<QuestionOutputDto>? Questions { get; set; }
    }
}
