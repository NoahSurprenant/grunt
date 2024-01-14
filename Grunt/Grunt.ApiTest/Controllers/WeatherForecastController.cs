using Microsoft.AspNetCore.Mvc;
using OpenSpartan.Grunt.Core;

namespace Grunt.ApiTest.Controllers;
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly HaloInfiniteClientFactory _haloInfiniteClientFactory;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, HaloInfiniteClientFactory haloInfiniteClientFactory)
    {
        _logger = logger;
        _haloInfiniteClientFactory = haloInfiniteClientFactory;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        var c = await _haloInfiniteClientFactory.CreateAsync();
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
