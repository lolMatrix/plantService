using DataBase;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlantService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GreenhoseController : ControllerBase
    {
        private readonly ILogger<GreenhoseController> _logger;
        private readonly Repository<Greenhose> _repository;

        public GreenhoseController(ILogger<GreenhoseController> logger, Repository<Greenhose> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        /// <summary>
        /// Возврщает все записи теплиц
        /// </summary>
        /// <response code="200">Возращает массив записей Теплиц</response>
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(_repository.GetAll());
        }

        /// <summary>
        /// Находит теплицу по id
        /// </summary>
        /// <param name="id">id теплицы</param>
        /// <response code="200">Возращает запись Теплицы</response>
        /// <response code="404">Если теплица не найдена</response>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var model = _repository.GetById(id);

            if (model != null)
                return new JsonResult(model);

            return NotFound();
        }

        /// <summary>
        /// Создает запись о теплице
        /// </summary>
        /// <param name="greenhose">Запись, которую необходимо создать</param>
        /// <response code="200">Возращает созданную запись Теплицы</response>
        [HttpPost("create")]
        public IActionResult Post([FromBody] Greenhose greenhose)
        {
            greenhose = _repository.Save(greenhose);
            return new JsonResult(greenhose);
        }

        /// <summary>
        /// Редактирует запись о теплице
        /// </summary>
        /// <param name="greenhose">Запись</param>
        /// <response code="200">Возращает обновленную запись Теплицы</response>
        [HttpPut("update")]
        public IActionResult Update([FromBody] Greenhose greenhose)
        {
            greenhose = _repository.Update(greenhose);

            return new JsonResult(greenhose);
        }

        /// <summary>
        /// Удаляет теплицу, если такая найдена
        /// </summary>
        /// <param name="id">id теплицы</param>
        /// <response code="200">Если теплица удалена</response>
        /// <response code="404">Если теплица не найдена</response>
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var model = _repository.GetById(id);

            if (model != null)
                return NotFound();

            _repository.Delete(model);
            return Ok();
        }
    }
}
