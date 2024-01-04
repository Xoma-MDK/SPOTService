using Newtonsoft.Json;

namespace SPOTService.DataStorage.Entities
{
    public class Group : IEntity
    {
        public int Id { get; set; }
        public required string Title { get; set; }

        [JsonIgnore]
        public virtual Respondent? Respondent { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<Survey>? Surveys { get; set; }
    }
}
