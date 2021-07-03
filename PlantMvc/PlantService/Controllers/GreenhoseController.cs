using DataBase;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlantService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GreenhoseController : ControllerBase
    {
        private readonly ILogger<GreenhoseController> _logger;
        private readonly Repository<Greenhose> _repository;

        public GreenhoseController(ILogger<GreenhoseController> logger, Repository<Greenhose> repository)
        {
            _logger = logger;
            _repository = repository;
        }


        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(_repository.GetAll());
        }

        [HttpGet("/{id}")]
        public IActionResult Get(int id)
        {
            return new JsonResult(_repository.GetById(id));
        }

        [HttpPost("/create")]
        public IActionResult Post(string json)
        {
            Greenhose greenhose = JsonConvert.DeserializeObject<Greenhose>(json);
            greenhose = _repository.Save(greenhose);
            return new JsonResult(greenhose);
        }

        [HttpPut("/update")]
        public IActionResult Update(string json)
        {
            Greenhose greenhose = JsonConvert.DeserializeObject<Greenhose>(json);

            greenhose = _repository.Update(greenhose);

            return new JsonResult(greenhose);
        }

        [HttpDelete("/delete/{id}")]
        public IActionResult Delete(int id)
        {
            _repository.Delete(_repository.GetById(id));
            return Ok();
        }
    }
}
