using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NLog.Web;
using SPOTService.DataStorage;
using SPOTService.Extensions;
using SPOTService.Infrastructure.HostedServices;
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
                    (LogLevel.Trace);
    builder.Host.UseNLog();
}


void ConfigureServices(IServiceCollection services)
{
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    services.AddSwaggerGen();
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = AuthOptions.ISSUER,
            ValidateAudience = true,
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
    services.AddControllers();
    services.AddHostedService<TelegramBot>();
}

void ConfigureApp(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    app.SeedData();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CoreService v1"));

    app.UseCors(builder => builder.AllowAnyOrigin());
    app.UseRouting();
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });

}



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

void AddDbContext(IServiceCollection services)
{
    services.AddTransient<MainContext>();
}