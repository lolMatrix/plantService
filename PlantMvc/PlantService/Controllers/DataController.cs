using DataBase;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantService.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class DataController : ControllerBase
    {
        private readonly ILogger<DataController> _logger;
        private readonly Repository<Data> _repository;
        private readonly Repository<Sensor> _sensors;

        public DataController(ILogger<DataController> logger, Repository<Data> repository, Repository<Sensor> sensors)
        {
            _logger = logger;
            _repository = repository;
            _sensors = sensors;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return new JsonResult(_repository.GetAll());
        }

        [HttpGet("/from/{id}")]
        public IActionResult GetBySensorId(int id)
        {
            var sensorData = _repository.Select(x => x.Sensor.Id == id);
            return new JsonResult(sensorData);
        }

        [HttpPost("/register")]
        public IActionResult RegisterData([FromBody] Data data)
        {
            data = _repository.Save(data);
            return new JsonResult(data);
        }
    }
}
