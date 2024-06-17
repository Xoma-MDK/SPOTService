using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPOTService.DataStorage.Entities;

namespace SPOTService.DataStorage.Configurations
{
    /// <summary>
    /// Конфигурация сущности Question для Entity Framework Core.
    /// </summary>
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        /// <summary>
        /// Настраивает схему (структуру) таблицы в базе данных для сущности Question.
        /// </summary>
        /// <param name="entity">Конфигурация сущности Question.</param>
        public void Configure(EntityTypeBuilder<Question> entity)
        {
            // Указываем таблицу, с которой будет соответствовать сущность Question
            entity.ToTable(nameof(Question));

            // Указываем первичный ключ
            entity.HasKey(e => e.Id);

            // Настраиваем свойства сущности Question
            entity.Property(e => e.Id).IsRequired(true); // Id обязателен и автоинкрементируемый
            entity.Property(e => e.Title).IsRequired(true); // Заголовок (Title) обязателен

            // Устанавливаем отношение многие ко многим между Question и AnswerVariant через промежуточную таблицу QuestionAnswerVariant
            entity.HasMany(e => e.AnswerVariants)
                  .WithMany(e => e.Questions)
                  .UsingEntity<QuestionAnswerVariant>(
                      x => x.HasOne(e => e.AnswerVariant).WithMany(e => e.QuestionAnswerVariants),
                      t => t.HasOne(e => e.Question).WithMany(e => e.QuestionAnswerVariants)
                  );

            // Дополнительные настройки можно добавить здесь, если необходимо
        }
    }
}
