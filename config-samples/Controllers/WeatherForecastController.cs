using Microsoft.AspNetCore.Mvc;

namespace config_samples.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    // private static readonly string[] Summaries = new[]
    // {
    //     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    // };

    private readonly ILogger<WeatherForecastController> logger;
    private readonly IConfiguration configuration;

    public WeatherForecastController(ILogger<WeatherForecastController> logger,  IConfiguration config)
    {
        this.logger = logger;
        this.configuration = config;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        //Get WeatherStation - Brute force method
        string name = this.configuration["WeatherStation:Name"];
        string location = this.configuration["WeatherStation:Location"];
        WeatherStation station2  = new WeatherStation{
            Name = name,
            Location = location
        }; 

        // Get WeatherStation - a little more elegant       
        WeatherStation station = this.configuration.GetSection("WeatherStation").Get<WeatherStation>();
        
        // Get Summaries       
        string[] Summaries = this.configuration.GetSection("Summaries").Get<string[]>();
        
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)],
            weatherStation = station
        })
        .ToArray();
    }
}
