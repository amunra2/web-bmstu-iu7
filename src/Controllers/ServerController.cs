using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ServerING.DTO;
using ServerING.Models;
using ServerING.Services;

namespace ServerING.Controllers {
    [ApiController]
    [Route("/api/v1/server")]
    public class ServerController : Controller {
        private IServerService serverService;

        public ServerController(IServerService serverService) {
            this.serverService = serverService;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] ServerFilterDto filter,
                [FromQuery] ServerSortState sortState,
                [FromQuery] int page) {
            return Ok(serverService.GetAllServers(filter, sortState, page));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) {
            Console.WriteLine(id);
            return Ok(serverService.GetServerByID(id));
        }

        [HttpPost]
        public void Add(ServerFormDto server) {
            // Console.WriteLine(server.Name);
            // return Ok(serverService.AddServer(server));
        }

        [HttpPatch("{id}")]
        public void Update(int id, ServerFormDto server) {
            Console.WriteLine(id);
        }

        [HttpDelete("{id}")]
        public void Delete(int id) {
            Console.WriteLine(id);
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