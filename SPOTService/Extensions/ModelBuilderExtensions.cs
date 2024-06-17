using Microsoft.EntityFrameworkCore;
using SPOTService.DataStorage.Entities;

namespace SPOTService.Extensions
{
    /// <summary>
    /// Расширения для ModelBuilder.
    /// </summary>
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// Инициализирует начальные данные в модели.
        /// </summary>
        /// <param name="modelBuilder">Построитель модели.</param>
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.CreateRoles();
        }

        /// <summary>
        /// Создает начальные роли в модели данных.
        /// </summary>
        /// <param name="modelBuilder">Построитель модели.</param>
        static void CreateRoles(this ModelBuilder modelBuilder)
        {

            var roles = new List<Role>() {
                new()
                {
                    Id = 1,
                    Title = "admin"
                },
                new()
                {
                    Id = 2,
                    Title = "user"
                }
            };
            modelBuilder.Entity<Role>().HasData(roles);
        }
    }
}
