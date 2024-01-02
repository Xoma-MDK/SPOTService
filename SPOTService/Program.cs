
using SPOTService.DataStorage;
using SPOTService.Extensions;

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
}


void ConfigureServices(IServiceCollection services)
{
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    services.AddSwaggerGen();
    services.AddControllers();
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