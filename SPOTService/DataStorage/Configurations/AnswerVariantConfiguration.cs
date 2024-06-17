using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPOTService.DataStorage.Entities;

namespace SPOTService.DataStorage.Configurations
{
    /// <summary>
    /// Конфигурация сущности AnswerVariant для Entity Framework Core.
    /// </summary>
    public class AnswerVariantConfiguration : IEntityTypeConfiguration<AnswerVariant>
    {
        /// <summary>
        /// Настраивает схему (структуру) таблицы в базе данных для сущности AnswerVariant.
        /// </summary>
        /// <param name="entity">Конфигурация сущности AnswerVariant.</param>
        public void Configure(EntityTypeBuilder<AnswerVariant> entity)
        {
            // Указываем таблицу, с которой будет соответствовать сущность AnswerVariant
            entity.ToTable(nameof(AnswerVariant));

            // Указываем первичный ключ
            entity.HasKey(e => e.Id);

            // Настраиваем свойства сущности AnswerVariant
            entity.Property(e => e.Id).IsRequired(true); // Id обязателен и авто инкрементируемый
            entity.Property(e => e.Title).IsRequired(true); // Заголовок (Title) обязателен

            // Дополнительные настройки можно добавить здесь, если необходимо
        }
    }
}
