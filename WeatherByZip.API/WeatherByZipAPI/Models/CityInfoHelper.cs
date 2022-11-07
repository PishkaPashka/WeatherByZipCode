namespace WeatherByZipAPI.Models;

public class CityInfoHelper
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Main Main { get; set; }
}

public class Main
{
    public double Temp { get; set; }
}
