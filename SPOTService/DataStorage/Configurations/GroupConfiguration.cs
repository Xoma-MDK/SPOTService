using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPOTService.DataStorage.Entities;

namespace SPOTService.DataStorage.Configurations
{
    /// <summary>
    /// Конфигурация сущности Group для Entity Framework Core.
    /// </summary>
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        /// <summary>
        /// Настраивает схему (структуру) таблицы в базе данных для сущности Group.
        /// </summary>
        /// <param name="entity">Конфигурация сущности Group.</param>
        public void Configure(EntityTypeBuilder<Group> entity)
        {
            // Указываем таблицу, с которой будет соответствовать сущность Group
            entity.ToTable(nameof(Group));

            // Указываем первичный ключ
            entity.HasKey(e => e.Id);

            // Настраиваем свойства сущности Group
            entity.Property(e => e.Id).IsRequired(true); // Id обязателен и автоинкрементируемый
            entity.Property(e => e.Title).IsRequired(true); // Заголовок (Title) обязателен

            // Отношения (например, с Respondent и Survey) указывать здесь не требуется, 
            // так как они не определены в сущности Group

            // Дополнительные настройки можно добавить здесь, если необходимо
        }
    }
}
