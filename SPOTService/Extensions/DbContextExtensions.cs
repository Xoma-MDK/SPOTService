using Microsoft.EntityFrameworkCore;

namespace SPOTService.Extensions
{
    public static class DbContextExtensions
    {
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
