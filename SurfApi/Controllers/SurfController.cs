using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
namespace SurfApi.Controllers;

[ApiController]
[Route("[controller]")]
public class SurfController : ControllerBase
{
    readonly HttpClient SurfHttpClient = new();
    readonly HttpClient GeoHttpClient = new();

    private readonly string ApiKey = "659a0bbc-a5b0-11ef-ae24-0242ac130003-659a0c8e-a5b0-11ef-ae24-0242ac130003";

    private readonly string ApiKeyGeo = "93f84c5d5bd64db5a41ee93c00efefa3";



    [HttpGet]
    public async Task<SurfResponse> Get(string placeName)
    {
        var url = $"https://api.geoapify.com/v1/geocode/search?text={placeName}&apiKey={ApiKeyGeo}";

        GeoHttpClient.DefaultRequestHeaders.Add("Authorization", ApiKey);
        var GeoCodeResponse = await GeoHttpClient.GetFromJsonAsync<GeoCodeResponse>
        (url);

        var longitude = GeoCodeResponse.features[0].properties.lon.ToString();
        var latiude = GeoCodeResponse.features[0].properties.lat.ToString();


        SurfHttpClient.DefaultRequestHeaders.Add("Authorization", ApiKey);
        var surfResponse = await SurfHttpClient.GetFromJsonAsync<SurfResponse>
        ($"https://api.stormglass.io/v2/weather/point?lat={latiude}&lng={longitude}&params=waveHeight");

        // var m = surfResponse.hours.Where(s => s.waveHeight.meteo > 1).Select(m => new
        // {
        //     Wavesover1 = m.waveHeight.meteo,
        //     timeAtWave = m.time
        // }).Select(wave => $"This wave is definitely over one meter{wave.Wavesover1} at time {wave.timeAtWave}");

        return surfResponse;

    }
}