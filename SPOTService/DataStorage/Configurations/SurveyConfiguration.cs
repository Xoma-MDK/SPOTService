using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPOTService.DataStorage.Entities;

namespace SPOTService.DataStorage.Configurations
{
    /// <summary>
    /// Конфигурация сущности Survey для Entity Framework Core.
    /// </summary>
    public class SurveyConfiguration : IEntityTypeConfiguration<Survey>
    {
        /// <summary>
        /// Настраивает схему (структуру) таблицы в базе данных для сущности Survey.
        /// </summary>
        /// <param name="entity">Конфигурация сущности Survey.</param>
        public void Configure(EntityTypeBuilder<Survey> entity)
        {
            // Указываем таблицу, с которой будет соответствовать сущность Survey
            entity.ToTable(nameof(Survey));

            // Указываем первичный ключ
            entity.HasKey(x => x.Id);

            // Настраиваем обязательные свойства сущности Survey
            entity.Property(x => x.Id).IsRequired(true); // Id обязателен и авто инкрементируемый
            entity.Property(x => x.Title).IsRequired(true); // Title обязателен
            entity.Property(x => x.AccessCode).IsRequired(true); // AccessCode обязателен
            entity.Property(x => x.Department).IsRequired(true); // Department обязателен
            entity.Property(x => x.GroupId).IsRequired(true); // GroupId обязателен
            entity.Property(x => x.UserId).IsRequired(true); // UserId обязателен

            // Настраиваем связь многие ко многим между Survey и Question через промежуточную таблицу SurveyQuestion
            entity.HasMany(x => x.Questions)
                  .WithMany(x => x.Surveys)
                  .UsingEntity<SurveyQuestion>(
                      x => x.HasOne(x => x.Question).WithMany(x => x.SurveyQuestions),
                      x => x.HasOne(x => x.Survey).WithMany(x => x.SurveyQuestions)
                  );

            // Настраиваем связь один ко многим между Survey и Group
            entity.HasOne(x => x.Group)
                  .WithMany(x => x.Surveys)
                  .HasForeignKey(x => x.GroupId)
                  .OnDelete(DeleteBehavior.Cascade); // При удалении группы удаляются все связанные опросы

            // Настраиваем связь один ко многим между Survey и User
            entity.HasOne(x => x.User)
                  .WithMany(x => x.Surveys)
                  .HasForeignKey(x => x.UserId)
                  .OnDelete(DeleteBehavior.Cascade); // При удалении пользователя удаляются все связанные опросы
        }
    }
}
