using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
namespace SurfApi.Controllers;

[ApiController]
[Route("[controller]")]
public class SurfController : ControllerBase
{
    readonly HttpClient httpClient = new();

    private readonly string ApiKey = "ee7c013a-95fb-11ee-8b92-0242ac130002-ee7c01b2-95fb-11ee-8b92-0242ac130002";


    [HttpGet]
    public async Task<SurfResponse> Get(string latitude, string longitude)
    {
        httpClient.DefaultRequestHeaders.Add("Authorization", ApiKey);
        var surfResponse = await httpClient.GetFromJsonAsync<SurfResponse>
        ($"https://api.stormglass.io/v2/weather/point?lat={latitude}&lng={longitude}&params=waveHeight");

        return surfResponse;
    }
}