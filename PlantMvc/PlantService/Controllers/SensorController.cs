using DataBase;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace PlantService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SensorController : ControllerBase
    {
        private readonly ILogger<SensorController> _logger;
        private readonly SensorRepository _repository;

        public SensorController(ILogger<SensorController> logger, SensorRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpPut]
        [Route("/[controller]/update")]
        public IActionResult UpdateSensor(Sensor sensor)
        {
            _repository.Update(sensor);

            return new JsonResult(_repository.GetAll());
        }

        [HttpPost]
        [Route("/[controller]/register")]
        public IActionResult RegisterSensor([FromBody] Sensor sensor)
        {
            sensor = _repository.Save(sensor);
            return new JsonResult(sensor);
        }

        [HttpDelete]
        [Route("/[controller]/delete/{id}")]
        public IActionResult DeleteSensor(int id)
        {
            try
            {
                _repository.Delete(_repository.GetById(id));
            }
            catch (Exception e)
            {
                _logger.LogError("Не существует сенсора с таким id {id}", id);
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpGet]
        [Produces("application/json")]
        public IActionResult Get()
        {
            return new JsonResult(_repository.GetAll());
        }

        [HttpGet]
        [Route("/[controller]/{id}")]
        [Produces("application/json")]
        public IActionResult GetById(int id)
        {
            return new JsonResult(_repository.GetById(id));
        }
    }
}
