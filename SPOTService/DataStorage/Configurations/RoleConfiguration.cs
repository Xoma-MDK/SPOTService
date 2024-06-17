using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPOTService.DataStorage.Entities;

namespace SPOTService.DataStorage.Configurations
{
    /// <summary>
    /// Конфигурация сущности Role для Entity Framework Core.
    /// </summary>
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        /// <summary>
        /// Настраивает схему (структуру) таблицы в базе данных для сущности Role.
        /// </summary>
        /// <param name="entity">Конфигурация сущности Role.</param>
        public void Configure(EntityTypeBuilder<Role> entity)
        {
            // Указываем таблицу, с которой будет соответствовать сущность Role
            entity.ToTable(nameof(Role));

            // Указываем первичный ключ
            entity.HasKey(e => e.Id);

            // Настраиваем свойства сущности Role
            entity.Property(e => e.Id).IsRequired(true); // Id обязателен и авто инкрементируемый
            entity.Property(e => e.Title).IsRequired(true); // Title обязателен

            // Устанавливаем отношение между Role и Rules: многие ко многим через промежуточную таблицу RoleRules
            entity.HasMany(e => e.Rules)
                  .WithMany(e => e.Roles)
                  .UsingEntity<RoleRules>(
                      x => x.HasOne(x => x.Rule).WithMany(x => x.RoleRules), // От Rule к RoleRules
                      x => x.HasOne(x => x.Role).WithMany(x => x.RoleRules) // От Role к RoleRules
                  );
        }
    }
}
