using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using WeatherByZipAPI.Models;

namespace WeatherByZip.API;

public interface IWeatherService
{
    Task<CityInfo> GetCityInfoByZipCode(string zipCode, string countryCode = "us");
}

public class WeatherService : IWeatherService
{
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _clientFactory;
    private readonly JsonSerializerOptions _jsonSerOps = new JsonSerializerOptions()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public WeatherService(
        IConfiguration configuration,
        IHttpClientFactory clientFactory)
    {
        _configuration = configuration;
        _clientFactory = clientFactory;
    }
    public async Task<CityInfo> GetCityInfoByZipCode(string zipCode, string countryCode)
    {
        var cityInfo = await GetCityName(zipCode, countryCode);
        return cityInfo;
    }

    private async Task<CityInfo> GetCityName(string zipCode, string countryCode)
    {
        if (string.IsNullOrWhiteSpace(zipCode))
            throw new ValidationException(nameof(zipCode));

        var queryString = QueryHelpers.AddQueryString(
            _configuration["OpenWeatherUrl"],
            new Dictionary<string, string>()
            {
                {"zip", $"{zipCode},{countryCode}" },
                {"APPID", _configuration["OpenWeatherApiKey"] }
            });

        var response = await GetResponse(queryString);

        if (!response.IsSuccessStatusCode)
            throw new ValidationException(nameof(zipCode));

        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<CityInfoHelper>(responseString, _jsonSerOps);
        result.Main.Temp -= 273;

        var cityInfo = new CityInfo()
        {
            Name = result.Name,
            Temperature = result.Main.Temp
        };


        return cityInfo;
    }

    private async Task<HttpResponseMessage> GetResponse(string uri)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        var client = _clientFactory.CreateClient();

        return await client.SendAsync(request);
    }
}
