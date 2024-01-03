﻿

using Newtonsoft.Json;

namespace SPOTService.DataStorage.Entities
{
    public class Question : IEntity
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public bool IsOpen { get; set; }

        [JsonIgnore]
        public virtual Answer? Answer { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<AnswerVariant>? AnswerVariants { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<QuestionAnswerVariant>? QuestionAnswerVariants { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<Survey>? Surveys { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<SurveyQuestion>? SurveyQuestions { get; set; }
    }
}