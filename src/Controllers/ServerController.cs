using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ServerING.Models;
using ServerING.Services;

namespace ServerING.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class ServerController : Controller {
        private IServerService serverService;

        public ServerController(IServerService serverService) {
            this.serverService = serverService;
        }

        [HttpGet(Name = "all")]
        public IEnumerable<Server> GetAll()
        {
            return serverService.GetAllServers();
        }

    }
}

// [ApiController]
// [Route("[controller]")]
// public class WeatherForecastController : ControllerBase
// {
//     private static readonly string[] Summaries = new[]
//     {
//         "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//     };

//     private readonly ILogger<WeatherForecastController> _logger;

//     public WeatherForecastController(ILogger<WeatherForecastController> logger)
//     {
//         _logger = logger;
//     }

//     [HttpGet(Name = "GetWeatherForecast")]
//     public IEnumerable<WeatherForecast> Get()
//     {
//         return Enumerable.Range(1, 5).Select(index => new WeatherForecast
//         {
//             Date = DateTime.Now.AddDays(index),
//             TemperatureC = Random.Shared.Next(-20, 55),
//             Summary = Summaries[Random.Shared.Next(Summaries.Length)]
//         })
//         .ToArray();
//     }
// }