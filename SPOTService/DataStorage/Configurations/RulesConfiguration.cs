using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPOTService.DataStorage.Entities;

namespace SPOTService.DataStorage.Configurations
{
    /// <summary>
    /// Конфигурация сущности Rules для Entity Framework Core.
    /// </summary>
    public class RulesConfiguration : IEntityTypeConfiguration<Rules>
    {
        /// <summary>
        /// Настраивает схему (структуру) таблицы в базе данных для сущности Rules.
        /// </summary>
        /// <param name="entity">Конфигурация сущности Rules.</param>
        public void Configure(EntityTypeBuilder<Rules> entity)
        {
            // Указываем таблицу, с которой будет соответствовать сущность Rules
            entity.ToTable(nameof(Rules));

            // Указываем первичный ключ
            entity.HasKey(x => x.Id);

            // Настраиваем свойства сущности Rules
            entity.Property(x => x.Id).IsRequired(true); // Id обязателен и авто инкрементируемый
            entity.Property(e => e.Title).IsRequired(true); // Title обязателен
        }
    }
}
