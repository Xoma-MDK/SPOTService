using SPOTService.DataStorage;


namespace SPOTService.Extensions
{
    public static class DataSeedExtensions
    {
        private static IServiceProvider? _provider;

        public static void SeedData(this IApplicationBuilder builder)
        {
            _provider = builder.ApplicationServices;

            using (IServiceScope scope = _provider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                MainContext context = _provider.GetService<MainContext>()!;
                context!.Migrate();
            }
        }
    }
}
