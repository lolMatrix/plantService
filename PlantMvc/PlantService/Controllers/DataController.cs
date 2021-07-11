using DataBase;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StatisticAndSolutions;
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
        private readonly StatisticHandler _statistic;

        public DataController(ILogger<DataController> logger, Repository<Data> repository, StatisticHandler statistic)
        {
            _logger = logger;
            _repository = repository;
            _statistic = statistic;
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

        [HttpGet("get")]
        public IActionResult GetDataForPeriod(int sensorId, DateTime first, DateTime second)
        {
            var data = _statistic.GetDataFromSensorForPeriodById(first, second, sensorId);

            if (data?.Length == 0)
                return NotFound();

            return new JsonResult(data);
        }
    }
}