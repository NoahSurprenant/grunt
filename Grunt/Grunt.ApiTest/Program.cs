using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using OpenSpartan.Grunt.Util;

namespace Grunt.ApiTest;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services
            .AddHealthChecksUI(s =>
            {
                s.SetEvaluationTimeInSeconds(60);
                s.SetApiMaxActiveRequests(1);
                s.MaximumHistoryEntriesPerEndpoint(120);
                s.AddHealthCheckEndpoint("health-check", "health");
            })
            .AddInMemoryStorage()
            .Services
            .AddHealthChecks()
            .AddCheck<InfiniteClientHealthCheck>("InfiniteClientHealthCheck");

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.RegisterHaloInfiniteClientFactory();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        //app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.UseRouting()
            .UseEndpoints(config =>
            {
                config.MapHealthChecks("health", new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                config.MapHealthChecksUI(o =>
                {
                    o.UseRelativeApiPath = true;
                });
                config.MapDefaultControllerRoute();
            });

        app.Run();
    }
}
