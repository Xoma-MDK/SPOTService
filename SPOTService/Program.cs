using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog.Web;
using SPOTService.DataStorage;
using SPOTService.DataStorage.Repositories;
using SPOTService.Extensions;
using SPOTService.Infrastructure.HostedServices.TelegramBot;
using SPOTService.Infrastructure.InternalServices.Auth;
using SPOTService.Infrastructure.InternalServices.Auth.Constants;
using SPOTService.Infrastructure.InternalServices.Auth.ENums;


var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
AddDbContext(builder.Services);
ConfigureHost();
ConfigureServices(builder.Services);

var app = builder.Build();

ConfigureApp(app, app.Environment);


app.Run();
void ConfigureHost()
{

    int port = configuration.GetValue<int>("AppSettings:Port");
    builder.WebHost.UseKestrel(options => options.ListenAnyIP(port));
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel
                    (LogLevel.Information);
    builder.Host.UseNLog();
}


void ConfigureServices(IServiceCollection services)
{
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    services.AddSwaggerGen(opt => {
        opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "bearer"
        }
        );
        opt.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
            }
        });
        var filePath = Path.Combine(AppContext.BaseDirectory, "SPOTService.xml");
        opt.SwaggerDoc("v1", new OpenApiInfo { Title = "SPOTService", Version = "v1" });
        opt.IncludeXmlComments(filePath);

    });

    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidIssuer = AuthOptions.ISSUER,
            ValidateAudience = false,
            ValidAudience = AuthOptions.AUDIENCE,
            ValidateLifetime = true,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
        };
    }
    );

    services.AddAuthorizationBuilder()
        .AddPolicy(AuthPolicy.AccessPolicy, policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.RequireClaim(JwtClaimTypes.Scope, JwtTypes.Access);
        });

    services.AddAuthorization();
    services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
    services.AddCors(options =>
    {
        options.AddPolicy("AllowAllOrigins",
            builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
    });
    //Сервисы и репозитории
    //services.AddHostedService<BotService>();
    services.AddScoped<IAuthService, AuthService>();
    services.AddScoped<UserRepository>();
    services.AddScoped<GroupRepository>();
    services.AddScoped<SurveyRepository>();
    services.AddScoped<QuestionRepository>();
    services.AddScoped<AnswerVariantRepository>();
}

void ConfigureApp(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    app.SeedData();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SPOTService v1"));
    app.UseCors("AllowAllOrigins");
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });

}

void AddDbContext(IServiceCollection services)
{
    services.AddTransient<MainContext>();
}