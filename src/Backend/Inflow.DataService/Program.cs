using Microsoft.AspNetCore.Localization;
using Inflow.DataService.Middlewares;
using Inflow.DataService.Extensions;
using Inflow.Data.Options;

namespace Inflow.DataService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;

        services.AddControllers();
        services.AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .Configure<Configuration>(builder.Configuration)
            .AddLocalization(options => options.ResourcesPath = "Resources")
            .AddSingletonSqlOptions(
                builder.Configuration.GetValue<string>("SqlOptionsName") ??
                throw new InvalidOperationException(),
                builder.Configuration.GetConnectionString("DbConnectionString") ??
                throw new InvalidOperationException())
            .AddSingletonDatabaseProvider()
            .AddSingletonInflowDataQuery()
            .AddSingletonSqlSchema();          

        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger().UseSwaggerUI();
        }

        var appConfiguration = app.Configuration.Get<Configuration>();
        if (appConfiguration is null) throw new InvalidOperationException();
        var cultureName = appConfiguration.Culture;
        var supportedCultures = appConfiguration.SupportedCultures.ToList();

        app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(cultureName),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            })
            .UseCors(corsPolicyBuilder =>
            {
                corsPolicyBuilder
                    .WithOrigins(appConfiguration.OriginForWhichAllowedAnyMethodAndAnyHeaderInCorsPolicy)
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            })
            .UseStaticFiles()
            .UseMiddleware<ExceptionHandler>()
            .UseHttpsRedirection()
            .UseAuthorization();
        app.MapControllers();

        var sqlOptions = app.Services.GetService<BaseSqlOptions>();
        if (sqlOptions is null) throw new InvalidOperationException();
        sqlOptions.OpenConnectionIfClosed();

        app.Run();
    }
}