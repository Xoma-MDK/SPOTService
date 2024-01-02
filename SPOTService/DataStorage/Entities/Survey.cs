using Newtonsoft.Json;

namespace SPOTService.DataStorage.Entities
{
    public class Survey : IEntity
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required string AccessCode { get; set; }
        public DateTimeOffset? StartTime { get; set; }
        public DateTimeOffset? EndTime { get; set; }
        public bool Active { get; set; }
        public int GroupId { get; set; }
        public string? Department {  get; set; }
        public int UserId { get; set; }

        [JsonIgnore]
        public virtual Group? Group { get; set; }

        [JsonIgnore]
        public virtual User? User { get; set; }
        [JsonIgnore]
        public virtual Answer? Answer { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<Question>? Question { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<SurveyQuestion>? SurveyQuestions { get; set; }
    }
}
