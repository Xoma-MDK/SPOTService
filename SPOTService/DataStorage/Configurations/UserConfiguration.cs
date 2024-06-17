using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPOTService.DataStorage.Entities;

namespace SPOTService.DataStorage.Configurations
{
    /// <summary>
    /// Конфигурация сущности User для Entity Framework Core.
    /// </summary>
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        /// <summary>
        /// Настраивает схему (структуру) таблицы в базе данных для сущности User.
        /// </summary>
        /// <param name="entity">Конфигурация сущности User.</param>
        public void Configure(EntityTypeBuilder<User> entity)
        {
            // Указываем таблицу, с которой будет соответствовать сущность User
            entity.ToTable(nameof(User));

            // Указываем первичный ключ
            entity.HasKey(x => x.Id);

            // Настраиваем обязательные свойства сущности User
            entity.Property(e => e.Id).IsRequired(true); // Id обязателен и авто инкрементируемый
            entity.Property(e => e.Surname).IsRequired(true); // Surname обязателен
            entity.Property(e => e.Name).IsRequired(true); // Name обязателен
            entity.Property(e => e.Login).IsRequired(true); // Login обязателен
            entity.Property(e => e.PasswordHash).IsRequired(true); // PasswordHash обязателен
            entity.Property(e => e.RoleId).IsRequired(true); // RoleId обязателен

            // Настраиваем связь один ко многим между User и Role
            entity.HasOne(e => e.Role)
                  .WithMany(e => e.Users)
                  .HasForeignKey(e => e.RoleId)
                  .OnDelete(DeleteBehavior.Cascade); // При удалении роли удаляются все связанные пользователи
        }
    }
}
