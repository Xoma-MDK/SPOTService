
using Newtonsoft.Json;

namespace SPOTService.DataStorage.Entities
{
    public class SurveyQuestion : IEntity
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int SurveyId { get; set; }

        [JsonIgnore]
        public virtual Question? Question { get; set;}
        [JsonIgnore]
        public virtual Survey? Survey { get; set; }
    }
}
