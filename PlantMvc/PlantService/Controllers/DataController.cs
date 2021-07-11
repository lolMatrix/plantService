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

        /// <summary>
        /// Получает все данные, которые есть в базе данных
        /// </summary>
        /// <response code="200">Возвращает массив данных</response>
        [HttpGet]
        public IActionResult Index()
        {
            return new JsonResult(_repository.GetAll());
        }

        /// <summary>
        /// Получить сохраненные данные с сенсора
        /// </summary>
        /// <param name="id">id сенсора</param>
        /// <response code="200">Возвращает массив данных с сенсора</response>
        /// <response code="404">Если данные не найдены или сенсора не существует</response>
        [HttpGet("/from/{id}")]
        public IActionResult GetBySensorId(int id)
        {
            var sensorData = _repository.Select(x => x.Sensor.Id == id);

            if (sensorData?.Length > 0)
                return new JsonResult(sensorData);

            return NotFound();
        }

        /// <summary>
        /// Регистрация данных с датчиков
        /// </summary>
        /// <param name="data">Данные с датчика</param>
        /// <response code="200">Возвращается созданная в бд запись данных с датчика</response>
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