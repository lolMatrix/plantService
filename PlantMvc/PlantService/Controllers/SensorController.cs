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
        private readonly Repository<Sensor> _repository;

        public SensorController(ILogger<SensorController> logger, Repository<Sensor> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        /// <summary>
        /// Обновляет сенсор 
        /// </summary>
        /// <param name="sensor">Сенсор, который необходимо обновить</param>
        /// <response code="200">Возвращает обнавленный сенсор</response>
        [HttpPut("update")]
        public IActionResult UpdateSensor(Sensor sensor)
        {
            _repository.Update(sensor);

            return new JsonResult(_repository.GetAll());
        }

        /// <summary>
        /// Зарегистрировать сенсор в системе
        /// </summary>
        /// <param name="sensor">Сенсор, который необходимо зарегистрировать</param>
        /// <response code="200">Возвращает созданный сенсор</response>
        [HttpPost("register")]
        public IActionResult RegisterSensor([FromBody] Sensor sensor)
        {
            sensor = _repository.Save(sensor);
            return new JsonResult(sensor);
        }

        /// <summary>
        /// Удаляет сенсор из базы данных
        /// </summary>
        /// <param name="id">id сенсора </param>
        /// <response code="200">Если сенсор был удален</response>
        /// <response code="404">Если сенсор не был найден</response>
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteSensor(int id)
        {
            var model = _repository.GetById(id);

            if (model == null)
                return NotFound();

            _repository.Delete(model);

            return Ok();
        }

        /// <summary>
        /// Получить все сенсоры
        /// </summary>
        /// <response code="200">Возвращает все сенсоры</response>
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(_repository.GetAll());
        }

        /// <summary>
        /// Получить сенсор по id
        /// </summary>
        /// <param name="id">id Сенсора</param>
        /// <response code="200">Возвращает сенсор</response>
        /// <response code="404">Если сенсор не найден</response>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var sensor = _repository.GetById(id);
            
            if (sensor != null)
                return new JsonResult(sensor);

            return NotFound();
        }
    }
}
