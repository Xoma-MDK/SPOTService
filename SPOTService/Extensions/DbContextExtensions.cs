using Microsoft.EntityFrameworkCore;

namespace SPOTService.Extensions
{
    /// <summary>
    /// Расширения для DbContext.
    /// </summary>
    public static class DbContextExtensions
    {
        /// <summary>
        /// Проверяет соединение с базой данных.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        /// <returns>Возвращает true, если соединение успешно установлено, иначе false.</returns>
        public static bool TestConnection(this DbContext context)
        {
            var conn = context.Database.GetDbConnection();

            try
            {
                conn.Open();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
