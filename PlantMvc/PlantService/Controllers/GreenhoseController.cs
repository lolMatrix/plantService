using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlantService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GreenhoseController : ControllerBase
    {
        private readonly ILogger<GreenhoseController> _logger;

        private List<Greenhose> _greenhoses = new List<Greenhose>()
        {
            new Greenhose()
            {
                Name = "The One",
                Location = "vstu",
                Beds = new List<GardenBed>()
                {
                    new GardenBed()
            {
                Name = "1",
                WaterValuem = 10,
                Sensors = new List<Sensor>
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
                }
            }
                }
            }
        };

        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(_greenhoses);
        }

        // GET api/<GreenhouseController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id < 0 || id > _greenhoses.Count)
            {
                _logger.LogError("Теплици с id {id} не существует", id);
                return BadRequest($"Теплици с id {id} не существует");
            }

            return new JsonResult(_greenhoses[id]);
        }

        // POST api/<GreenhouseController>
        [HttpPost("/create")]
        public IActionResult Post(string json)
        {
            Greenhose greenhose = JsonConvert.DeserializeObject<Greenhose>(json);
            _greenhoses.Add(greenhose);
            return new JsonResult(greenhose);
        }

        // PUT api/<GreenhouseController>/5
        [HttpPut("/update")]
        public IActionResult Put(string json)
        {
            Greenhose greenhose = JsonConvert.DeserializeObject<Greenhose>(json);
            
            if (greenhose.Id < 0 || greenhose.Id > _greenhoses.Count)
            {
                _logger.LogError("Теплици с id {id} не существует", greenhose.Id);
                return BadRequest($"Теплици с id {greenhose.Id} не существует");
            }

            _greenhoses.Remove(_greenhoses[greenhose.Id]);
            _greenhoses.Insert(greenhose.Id, greenhose);

            return new JsonResult(greenhose);
        }

        // DELETE api/<GreenhouseController>/5
        [HttpDelete("/delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (id < 0 || id > _greenhoses.Count)
            {
                _logger.LogError("Теплици с id {id} не существует", id);
                return BadRequest($"Теплици с id {id} не существует");
            }

            _greenhoses.Remove(_greenhoses[id]);
            return Ok();
        }
    }
}
