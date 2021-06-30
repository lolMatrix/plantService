using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace PlantService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BedController : ControllerBase
    {
        private readonly ILogger<BedController> _logger;

        private List<GardenBed> _beds = new List<GardenBed>()
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
            },
            new GardenBed(){Name = "2", WaterValuem = 20},
            new GardenBed(){Name = "3", WaterValuem = 30}
        };

        public BedController(ILogger<BedController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("/[controller]")]
        public IActionResult Index()
        {
            return new JsonResult(_beds);
        }

        [HttpGet]
        [Route("/[controller]/{id}")]
        public IActionResult Details(int id)
        {
            if(id < 0 || id > _beds.Count)
            {
                _logger.LogError("Грядка с id {id} не найдена", id);
                return NotFound($"Грядка с id {id} не найдена");
            }

            return new JsonResult(_beds[id]);
        }

        [HttpPost]
        [Route("/[controller]/register")]
        public ActionResult Create(string json)
        {
            GardenBed bed;

            try
            {
                bed = JsonConvert.DeserializeObject<GardenBed>(json);
            }
            catch(Exception e)
            {
                _logger.LogError("Ошибка преобразования: {message}", e.Message);
                return BadRequest();
            }

            _beds.Add(bed);
            return new JsonResult(bed);
        }


        [HttpPut]
        [Route("/[controller]/edit")]
        public IActionResult Edit(string json)
        {
            GardenBed bed;

            try
            {
                bed = JsonConvert.DeserializeObject<GardenBed>(json);
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка преобразования: {message}", e.Message);
                return BadRequest();
            }

            if (bed.Id < 0 || bed.Id > _beds.Count)
            {
                _logger.LogError("Грядка с id {id} не найдена", bed.Id);
                return NotFound($"Грядка с id {bed.Id} не найдена");
            }

            _beds.Remove(_beds[bed.Id]);
            _beds.Insert(bed.Id, bed);

            return new JsonResult(bed);
        }

        [HttpDelete]
        [Route("/[controller]/delete")]
        public ActionResult Delete(int id)
        {
            if(id < 0 || id > _beds.Count)
            {
                _logger.LogError("Грядка с id {id} не найдена", id);
                return NotFound($"Грядка с id {id} не найдена");
            }

            _beds.Remove(_beds[id]);

            return Ok();
        }

    }
}
