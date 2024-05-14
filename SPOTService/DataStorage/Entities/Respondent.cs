using Newtonsoft.Json;

namespace SPOTService.DataStorage.Entities
{
    public class Respondent : IEntity
    {
        public int Id { get; set; }
        public string? Surname { get; set; }
        public string? Name { get; set; }
        public string? Patronomic {  get; set; }
        public int? GroupId { get; set; }
        public long TelegramId { get; set; }
        public long TelegramChatId { get; set; }
        public int? StateId { get; set; }

        [JsonIgnore]
        public virtual Group? Group { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<Answer>? Answers { get; set; }
    }
}
