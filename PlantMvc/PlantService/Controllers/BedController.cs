using DataBase;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PlantService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BedController : ControllerBase
    {
        private readonly ILogger<BedController> _logger;

        private readonly Repository<GardenBed> _repository;

        public BedController(ILogger<BedController> logger, Repository<GardenBed> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        /// <summary>
        /// Возврщает все записи грядок
        /// </summary>
        /// <response code="200">Возращает массив записей грядок</response>
        [HttpGet]
        public IActionResult Index()
        {
            return new JsonResult(_repository.GetAll());
        }

        /// <summary>
        /// Возвращает массив грядок по id теплицы 
        /// </summary>
        /// <param name="greenhoseId">id теплицы</param>
        /// <response code="200">Возращает массив грядок</response>
        /// <response code="404">Если грядки не найдены</response>
        [HttpGet("get/{greenhoseId}")]
        public IActionResult GetBedByGreenhoseId(int greenhoseId)
        {
            var beds = _repository.Select(x => x.GreenhoseId == greenhoseId);

            if (beds?.Length == 0)
            {
                return NotFound();
            }

            return new JsonResult(beds);
        }

        /// <summary>
        /// Находит грядку по id
        /// </summary>
        /// <param name="id">id грядки</param>
        /// <response code="200">Возращает запись грядки</response>
        /// <response code="404">Если теплица не найдена</response>
        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            var model = _repository.GetById(id);

            if (model != null)
                return new JsonResult(model);

            return NotFound();
        }

        /// <summary>
        /// Создает запись о грядке
        /// </summary>
        /// <param name="bed">Запись, которую необходимо создать</param>
        /// <response code="200">Возращает созданную запись грядки</response>
        [HttpPost("register")]
        public ActionResult Create([FromBody] GardenBed bed)
        {
            var created = _repository.Save(bed);
            return new JsonResult(created);
        }

        /// <summary>
        /// Редактирует запись о грядке
        /// </summary>
        /// <param name="bed">Запись</param>
        /// <response code="200">Возращает обновленную запись Грядки</response>
        [HttpPut("update")]
        public IActionResult Edit([FromBody] GardenBed bed)
        {
            bed = _repository.Update(bed);

            return new JsonResult(bed);
        }

        /// <summary>
        /// Удаляет грядку из базы данных
        /// </summary>
        /// <param name="id">id грядки</param>
        /// <response code="200">Если грядка удалена</response>
        /// <response code="404">Если грядка не найдена</response>
        [HttpDelete("delete/{id}")]
        public ActionResult Delete(int id)
        {
            var model = _repository.GetById(id);

            if (model == null)
                return NotFound();

            _repository.Delete(model);
            return Ok();
        }

    }
}
