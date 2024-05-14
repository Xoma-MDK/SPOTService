﻿using SPOTService.Dto.Answers;
using SPOTService.Dto.Groups;
using SPOTService.Dto.Questions;
using SPOTService.Dto.User;

namespace SPOTService.Dto.Surveys
{
    /// <summary>
    /// Модель обновления данных опроса
    /// </summary>
    public class SurveyUpdateDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }
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
        public int CreatorId { get; set; }
        /// <summary>
        /// Вопросы
        /// </summary>
        public IEnumerable<QuestionUpdateDto>? Questions { get; set; }
    }
}
