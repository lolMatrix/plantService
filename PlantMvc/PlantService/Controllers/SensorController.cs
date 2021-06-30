using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SensorController : ControllerBase
    {
        private List<Sensor> _sensors = new List<Sensor>()
        {
            new Sensor()
            {
                Name = "thirst",
                Type = Entities.Enums.SensorType.TemperatureSensor,
                SensorData = 10
            },
            new Sensor()
            {
                Name = "second",
                Type = Entities.Enums.SensorType.TemperatureSensor,
                SensorData = 20
            },
            new Sensor()
            {
                Name = "therd",
                Type = Entities.Enums.SensorType.TemperatureSensor,
                SensorData = 30
            }
        };

        private readonly ILogger<SensorController> _logger;

        public SensorController(ILogger<SensorController> logger)
        {
            _logger = logger;
        }

        [HttpPut]
        [Route("/[controller]/update")]
        public IActionResult UpdateSensor(Sensor sensor)
        {
            if (sensor.Id < 0 || sensor.Id > _sensors.Count)
                return BadRequest();

            _sensors.Remove(_sensors[sensor.Id]);
            _sensors.Insert(sensor.Id, sensor);
            return new JsonResult(_sensors);
        }

        [HttpPost]
        [Route("/[controller]/register")]
        public IActionResult RegisterSensor([FromBody] Sensor sensor)
        {
           
            _sensors.Add(sensor);
            return new JsonResult(sensor);
        }

        [HttpDelete]
        [Route("/[controller]/delete/{id}")]
        public IActionResult DeleteSensor(int id)
        {
            if(id < 0 || id > _sensors.Count)
            {
                _logger.LogError("Запись, которую вы хотите удалить с id {id} не существует", id);
                return BadRequest();
            }

            _sensors.Remove(_sensors[id]);

            return Ok();
        }

        [HttpGet]
        [Produces("application/json")]
        public IActionResult Get()
        {
            return new JsonResult(_sensors);
        }

        [HttpGet]
        [Route("/[controller]/{id}")]
        [Produces("application/json")]
        public IActionResult GetById(int id)
        {
            if (id > _sensors.Count || id < 0)
            {
                _logger.LogError("Сенсора с id {id} не существует", id);
                return BadRequest();
            }

            return new JsonResult(_sensors[id]);
        }
    }
}
