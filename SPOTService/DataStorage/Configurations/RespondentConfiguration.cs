using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPOTService.DataStorage.Entities;

namespace SPOTService.DataStorage.Configurations
{
    /// <summary>
    /// Конфигурация сущности Respondent для Entity Framework Core.
    /// </summary>
    public class RespondentConfiguration : IEntityTypeConfiguration<Respondent>
    {
        /// <summary>
        /// Настраивает схему (структуру) таблицы в базе данных для сущности Respondent.
        /// </summary>
        /// <param name="entity">Конфигурация сущности Respondent.</param>
        public void Configure(EntityTypeBuilder<Respondent> entity)
        {
            // Указываем таблицу, с которой будет соответствовать сущность Respondent
            entity.ToTable(nameof(Respondent));

            // Указываем первичный ключ
            entity.HasKey(e => e.Id);

            // Настраиваем свойства сущности Respondent
            entity.Property(e => e.Id).IsRequired(true); // Id обязателен и авто инкрементируемый
            entity.Property(e => e.TelegramId).IsRequired(true); // TelegramId обязателен

            // Устанавливаем отношение между Respondent и Group: многие к одному
            entity.HasOne(e => e.Group)
                  .WithMany(e => e.Respondents)
                  .HasForeignKey(e => e.GroupId)
                  .OnDelete(DeleteBehavior.Cascade); // При удалении группы удаляются все респонденты этой группы
        }
    }
}
