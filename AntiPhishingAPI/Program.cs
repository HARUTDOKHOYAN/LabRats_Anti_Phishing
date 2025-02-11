using LearningASPweb.Configurations;
using Microsoft.EntityFrameworkCore;
using Serilog;
using LearningASPweb.Data;
using LearningASPweb.Middlewares;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString("TumoLubCybersecurityString");
        builder.Services.AddDbContext<AnitPhoshingDbContext>(options =>
        {
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });
        builder.Host.UseSerilog((ctx,lc) => lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

        ServiceLocatorConfig.ConfigureAppServices(builder);
        
        
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseMiddleware<ExeptionMiddleware>();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.UseSerilogRequestLogging(); // Աշխատացնում ենք serilog-ի հավաքելու համակարքը
        app.Run();
    }
}

