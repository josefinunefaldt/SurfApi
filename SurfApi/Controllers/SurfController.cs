using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
namespace SurfApi.Controllers;

[ApiController]
[Route("[controller]")]
public class SurfController : ControllerBase
{
    readonly HttpClient httpClient = new();
    readonly HttpClient GeohttpClient = new();

    private readonly string ApiKey = "ee7c013a-95fb-11ee-8b92-0242ac130002-ee7c01b2-95fb-11ee-8b92-0242ac130002";

    private readonly string ApiKeyGeo = "93f84c5d5bd64db5a41ee93c00efefa3";
    private readonly string url = $"https://api.geoapify.com/v1/geocode/search?text={placeName}&apiKey={ApiKeyGeo}";


    [HttpGet]
    public async Task<SurfResponse> Get(string placeName)
    {


        GeohttpClient.DefaultRequestHeaders.Add("Authorization", ApiKey);
        var GeoCodeResponse = await httpClient.GetFromJsonAsync<GeoCodeResponse>
        (url);

        var longi = GeoCodeResponse.features[0].properties.lon.ToString();
        var lati = GeoCodeResponse.features[0].properties.lat.ToString();


        httpClient.DefaultRequestHeaders.Add("Authorization", ApiKey);
        var surfResponse = await httpClient.GetFromJsonAsync<SurfResponse>
        ($"https://api.stormglass.io/v2/weather/point?lat={lati}&lng={longi}&params=waveHeight");

        return surfResponse;
    }
}