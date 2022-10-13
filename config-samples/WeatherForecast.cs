namespace config_samples;

public class WeatherForecast
{
    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }

    public WeatherStation? weatherStation {get; set;}
}

public class WeatherStation{
    public string Name { get; set; } = "";
    public string Location { get; set; } = "";
}
