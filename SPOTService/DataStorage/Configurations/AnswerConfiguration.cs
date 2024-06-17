using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPOTService.DataStorage.Entities;

namespace SPOTService.DataStorage.Configurations
{
    /// <summary>
    /// Конфигурация сущности Answer для Entity Framework Core.
    /// </summary>
    public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {
        /// <summary>
        /// Настраивает схему (структуру) таблицы в базе данных для сущности Answer.
        /// </summary>
        /// <param name="entity">Конфигурация сущности Answer.</param>
        public void Configure(EntityTypeBuilder<Answer> entity)
        {
            // Указываем таблицу, с которой будет соответствовать сущность Answer
            entity.ToTable(nameof(Answer));

            // Указываем первичный ключ
            entity.HasKey(e => e.Id);

            // Настраиваем свойства сущности Answer
            entity.Property(e => e.Id).IsRequired(true);
            entity.Property(e => e.SurveyId).IsRequired(true);
            entity.Property(e => e.QuestionId).IsRequired(true);
            entity.Property(e => e.AnswerVariantId).IsRequired(false); // Может быть null

            // Настраиваем связь с сущностью Survey (один ко многим)
            entity.HasOne(e => e.Survey)
                  .WithMany(e => e.Answers)
                  .HasForeignKey(e => e.SurveyId)
                  .OnDelete(DeleteBehavior.Cascade); // При удалении Survey каскадно удаляются связанные Answers

            // Настраиваем связь с сущностью Question (один ко многим)
            entity.HasOne(e => e.Question)
                  .WithMany(e => e.Answers)
                  .HasForeignKey(e => e.QuestionId)
                  .OnDelete(DeleteBehavior.Cascade); // При удалении Question каскадно удаляются связанные Answers

            // Настраиваем связь с сущностью AnswerVariant (один ко многим)
            entity.HasOne(e => e.AnswerVariant)
                  .WithMany(e => e.Answers)
                  .HasForeignKey(e => e.AnswerVariantId)
                  .OnDelete(DeleteBehavior.Cascade); // При удалении AnswerVariant каскадно удаляются связанные Answers

            // Настраиваем связь с сущностью Respondent (один ко многим)
            entity.HasOne(e => e.Respondent)
                  .WithMany(e => e.Answers)
                  .HasForeignKey(e => e.RespondentId)
                  .OnDelete(DeleteBehavior.Cascade); // При удалении Respondent каскадно удаляются связанные Answers
        }
    }
}
