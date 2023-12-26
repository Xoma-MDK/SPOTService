using System;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using SPOTService.Extensions;

namespace SPOTService.DataStorage
{
    public partial class MainContext : DbContext
    {
        private readonly ILogger<MainContext> _logger;
        public MainContext(ILogger<MainContext> logger)
        {
            _logger = logger;
        }

        public void Migrate()
        {
            if (!this.TestConnection())
            {
                _logger.LogDebug($"Database is NOT available.");
                return;
            }

            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
                .GetSection("ConnectionStrings")["Database"];
            optionsBuilder.UseLazyLoadingProxies().UseNpgsql(connectionString);
        }
    }
}
