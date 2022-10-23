using System.Linq;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using ErrorEventArgs = Microsoft.AspNetCore.Components.Web.ErrorEventArgs;

namespace Controller
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
            else
                return StatusCode(400, "Something went wrong");
        }

        [HttpGet]
        public async Task<IActionResult> GetExcuseById(int id)
        {
            Excuse excuseFromDb = await _service.GetExcuseById(id);

            if (excuseFromDb == null) return NotFound();
            return Ok(excuseFromDb);
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetExcuseById()
        {
            throw new NotImplementedException();
        }
    }
}