using SPOTService.DataStorage;


namespace SPOTService.Extensions
{
    /// <summary>
    /// Расширения для инициализации данных в приложении.
    /// </summary>
    public static class DataSeedExtensions
    {
        private static IServiceProvider? _provider;

        /// <summary>
        /// Инициализирует данные в базе данных приложения.
        /// </summary>
        /// <param name="builder">Строитель приложения ASP.NET Core.</param>
        public static void SeedData(this IApplicationBuilder builder)
        {
            _provider = builder.ApplicationServices;

            using var scope = _provider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetService<MainContext>();
            context?.Migrate();
        }
    }
}
