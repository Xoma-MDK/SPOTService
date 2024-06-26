﻿using SPOTService.Dto.AnswerVariants;

namespace SPOTService.Dto.Questions
{
    /// <summary>
    /// Модель обновления данных вопроса
    /// </summary>
    public class QuestionUpdateDto
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
        /// Флаг "открытости" вопроса
        /// </summary>
        public bool IsOpen { get; set; }
        /// <summary>
        /// Варианты ответов
        /// </summary>
        public IEnumerable<AnswerVariantUpdateDto>? AnswerVariants { get; set; }
    }
}
