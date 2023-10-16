using CharchoobApi.Application.WeatherForecasts.Queries.GetWeatherForecasts;
using Microsoft.AspNetCore.Mvc;

namespace CharchoobApi.Host.Controllers.V1;

/// <summary>
/// Weather Forecast
/// </summary>
[ApiVersion("1.0")]
public class WeatherForecastController : ApiControllerBase
{
    /// <summary>
    /// Get Weather forecast
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        return await Mediator.Send(new GetWeatherForecastsQuery());
    }
}
