using System;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using SPOTService.Extensions;
using SPOTService.DataStorage.Entities;

namespace SPOTService.DataStorage
{
    public partial class MainContext(ILogger<MainContext> logger) : DbContext
    {
        private readonly ILogger<MainContext> _logger = logger;

        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<AnswerVariant> AnswerVariants { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionAnswerVariant> QuestionAnswerVariants { get; set; }
        public virtual DbSet<Respondent> Respondents { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RoleRules> RoleRules { get; set; }
        public virtual DbSet<Rules> Rules { get; set; }
        public virtual DbSet<Survey> Surveys { get; set; }
        public virtual DbSet<SurveyQuestion> SurveyQuestions { get; set; }
        public virtual DbSet<User> Users { get; set; }
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
            modelBuilder.Seed();
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
