namespace WeatherByZipAPI.Models;

public class CityInfo
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Temperature{ get; set; }
    public DateTime RequestDate { get; set; }

    public string Date => RequestDate.ToString("g");
}
