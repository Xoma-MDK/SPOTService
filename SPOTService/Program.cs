
using NLog.Web;
using SPOTService.DataStorage;
using SPOTService.Infrastructure.HostedServices;

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
    services.AddControllers();
    services.AddHostedService<TelegramBot>();
}

void ConfigureApp(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
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