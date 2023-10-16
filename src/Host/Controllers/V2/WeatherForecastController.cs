using CharchoobApi.Application.WeatherForecasts.Queries.GetWeatherForecasts;
using CharchoobApi.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CharchoobApi.Host.Controllers.V2;

/// <summary>
/// Weather Forecast
/// </summary>
[ApiVersion("2.0")]
[Authorize(Roles = Sd.User)]
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
