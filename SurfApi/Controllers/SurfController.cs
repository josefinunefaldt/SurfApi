using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
namespace SurfApi.Controllers;

[ApiController]
[Route("[controller]")]
public class SurfController : ControllerBase
{
    readonly HttpClient SurfHttpClient = new();
    readonly HttpClient GeoHttpClient = new();

    private readonly string ApiKey = "ee7c013a-95fb-11ee-8b92-0242ac130002-ee7c01b2-95fb-11ee-8b92-0242ac130002";

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

        return surfResponse;
    }
}