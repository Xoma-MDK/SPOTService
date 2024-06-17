using Microsoft.EntityFrameworkCore;
using SPOTService.DataStorage.Entities;
using SPOTService.Extensions;
using System.Reflection;

namespace SPOTService.DataStorage
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="MainContext"/>.
    /// </summary>
    /// <param name="logger">Инстанс логгера для регистрации операций контекста базы данных.</param>
    public partial class MainContext(ILogger<MainContext> logger) : DbContext
    {
        private readonly ILogger<MainContext> _logger = logger;
        /// <summary>
        /// Ответы
        /// </summary>
        public virtual DbSet<Answer> Answers { get; set; }
        /// <summary>
        /// Варианты ответов
        /// </summary>
        public virtual DbSet<AnswerVariant> AnswerVariants { get; set; }
        /// <summary>
        /// Группы
        /// </summary>
        public virtual DbSet<Group> Groups { get; set; }
        /// <summary>
        /// Вопросы
        /// </summary>
        public virtual DbSet<Question> Questions { get; set; }
        /// <summary>
        /// Сущность связи вопросов и вариантов ответа
        /// </summary>
        public virtual DbSet<QuestionAnswerVariant> QuestionAnswerVariants { get; set; }
        /// <summary>
        /// Опрашиваемые
        /// </summary>
        public virtual DbSet<Respondent> Respondents { get; set; }
        /// <summary>
        /// Роли
        /// </summary>
        public virtual DbSet<Role> Roles { get; set; }
        /// <summary>
        /// Сущность связи прав и ролей
        /// </summary>
        public virtual DbSet<RoleRules> RoleRules { get; set; }
        /// <summary>
        /// Права
        /// </summary>
        public virtual DbSet<Rules> Rules { get; set; }
        /// <summary>
        /// Опросы
        /// </summary>
        public virtual DbSet<Survey> Surveys { get; set; }
        /// <summary>
        /// Сущность связи опросов и вопросов
        /// </summary>
        public virtual DbSet<SurveyQuestion> SurveyQuestions { get; set; }
        /// <summary>
        /// Пользователи
        /// </summary>
        public virtual DbSet<User> Users { get; set; }
        /// <summary>
        /// Выполняет миграцию базы данных на последнюю версию, если база данных доступна.
        /// </summary>
        public void Migrate()
        {
            if (!this.TestConnection())
            {
                _logger.LogDebug($"Database is NOT available.");
                return;
            }

            Database.Migrate();
        }

        /// <summary>
        /// Настраивает модели с использованием конфигураций из текущей сборки и инициализирует начальные данные.
        /// </summary>
        /// <param name="modelBuilder">Построитель моделей, используемый для настройки контекста.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Seed();
        }

        /// <summary>
        /// Настраивает параметры подключения к базе данных с использованием конфигурации приложения.
        /// </summary>
        /// <param name="optionsBuilder">Построитель опций для конфигурации контекста базы данных.</param>
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
