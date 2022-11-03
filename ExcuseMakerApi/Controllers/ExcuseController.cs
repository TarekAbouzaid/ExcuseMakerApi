using System.Linq;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using ErrorEventArgs = Microsoft.AspNetCore.Components.Web.ErrorEventArgs;

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
        public async Task<IActionResult> CreateExcuse(Excuse excuse)
        {
            var added = await _service.Add(excuse);
            if (added)
                return CreatedAtAction("GetExcuseById", new { id = excuse.Id }, excuse);
            return StatusCode(400, "Something went wrong");
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetExcuseById(int id)
        {
            Excuse excuseFromDb = await _service.GetExcuseById(id);

            if (excuseFromDb == null) return NotFound();
            return Ok(excuseFromDb);
        }

        [HttpGet]
        [Route("GetByCategory")]
        public async Task<IActionResult> GetExcuseByCategory(ExcuseCategory category)
        {
            var excusesFromDb = await _service.GetExcusesByCategory(category);

            if (excusesFromDb == null) return NotFound();
            return Ok(excusesFromDb);
        }

        [HttpGet]
        [Route("GetAllExcuses")]
        public async Task<IActionResult> GetAllExcuses()
        {
            var excusesFromDb = await _service.GetAllExcuses();

            if (excusesFromDb == null) return NotFound();
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
            Excuse randomExcuse = await _service.GetRandomExcuse(category);
            if (randomExcuse == null) return NotFound();
            return Ok(randomExcuse.Text);

        }
    }
}