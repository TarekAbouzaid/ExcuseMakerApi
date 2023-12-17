using DTO;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;

namespace ExcuseMakerApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class ExcuseController : ControllerBase
    {
        private readonly IExcuseService _service;

        public ExcuseController(IExcuseService service)
        {
            _service = service;
        }

        //localhost:44398/api/excuse

        [HttpPost] //Create Excuse
        public async Task<IActionResult> CreateExcuse(Excuse? excuse)
        {
            var added = await _service.Add(excuse);
            return added
                ? CreatedAtAction("GetExcuseById", new { id = excuse.Id }, excuse)
                : StatusCode(400, "Something went wrong");
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetExcuseById(int id)
        {
            var excuseFromDb = await _service.GetExcuseById(id);

            return Ok(excuseFromDb);
        }

        [HttpGet]
        [Route("GetByCategory")]
        public async Task<IActionResult> GetExcuseByCategory(ExcuseCategory category)
        {
            var excusesFromDb = await _service.GetExcusesByCategory(category);

            return Ok(excusesFromDb);
        }

        [HttpGet]
        [Route("GetAllExcuses")]
        public async Task<IActionResult> GetAllExcuses()
        {
            var excusesFromDb = await _service.GetAllExcuses();

            return Ok(excusesFromDb);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteExcuseById(int id)
        {
            var deleted = await _service.DeleteExcuse(id);
            if (!deleted) return NotFound();
            return Ok($@"deleted ({id})");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateExcuse(Excuse excuse)
        {
            var updated = await _service.UpdateExcuse(excuse);
            if (!updated) return NotFound();
            return Ok($@"updated ({excuse.Id})");
        }

        [HttpGet]
        [Route("GetRandomExcuse")]
        public async Task<IActionResult> GetRandomExcuse(ExcuseCategory category)
        {
            var randomExcuse = await _service.GetRandomExcuse(category);
            return Ok(randomExcuse.Text);
        }
    }
}