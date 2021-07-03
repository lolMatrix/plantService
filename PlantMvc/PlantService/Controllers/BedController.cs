using DataBase;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

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

        [HttpGet("/")]
        public IActionResult Index()
        {
            return new JsonResult(_repository.GetAll());
        }

        [HttpGet("/{id}")]
        public IActionResult Details(int id)
        {
            return new JsonResult(_repository.GetById(id));
        }

        [HttpPost("/register")]
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

            var created = _repository.Save(bed);
            return new JsonResult(created);
        }


        [HttpPut("/update")]
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

            bed = _repository.Update(bed);

            return new JsonResult(bed);
        }

        [HttpDelete("/delete/{id}")]
        public ActionResult Delete(int id)
        {
            _repository.Delete(_repository.GetById(id));
            return Ok();
        }

    }
}
