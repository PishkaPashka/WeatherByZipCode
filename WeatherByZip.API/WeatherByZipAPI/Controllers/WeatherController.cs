using Microsoft.AspNetCore.Mvc;
using WeatherByZip.API;
using WeatherByZipAPI.Models;

namespace WeatherByZipAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WeatherController : ControllerBase
{
    private readonly IWeatherService _weatherService;
    private readonly ICityInfoObjectService _cityInfoObjectService;

    public WeatherController(
        IWeatherService weatherService,
        ICityInfoObjectService cityInfoObjectService)
    {
        _weatherService = weatherService;
        _cityInfoObjectService = cityInfoObjectService;
    }

    [HttpGet]
    public async Task<ActionResult<List<CityInfo>>> GetRequestHistory()
    {
        return Ok(await _cityInfoObjectService.GetRequestHistory());
    }

    [Route("{zipCode}/{countryCode=us}"), HttpGet]
    public async Task<ActionResult<List<CityInfo>>> GetCityInfo(string zipCode, string countryCode)
    {
        var cityInfo = await _weatherService.GetCityInfoByZipCode(zipCode, countryCode);
        await _cityInfoObjectService.WriteRequestLog(cityInfo);
        return Ok(cityInfo);
    }
}
