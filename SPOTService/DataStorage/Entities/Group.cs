namespace SPOTService.DataStorage.Entities
{
    public class Group : IEntity
    {
        public int Id { get; set; }
        public required string Title { get; set; }

        public virtual IEnumerable<Respondent>? Respondents { get; set; }

        public virtual IEnumerable<Survey>? Surveys { get; set; }
    }
}
