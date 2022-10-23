using System.Linq;
using ExcuseMakerApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Controller
{
    [Route("api")]
    [ApiController]
    public class ExcuseController : ControllerBase
    {
        private readonly ExcuseContext _db;

        public ExcuseController(ExcuseContext db)
        {
            _db = db;
        }

        //localhost:44398/api/excuse

        [HttpPost] //Create Excuse
        public IActionResult CreateExcuse(Excuse excuse)
        {
            _db.Excuses.Add(excuse);
            _db.SaveChanges();
            WeatherForecast wetForecast = new WeatherForecast();
            return CreatedAtAction("GetExcuseById", new { id = excuse.Id }, excuse);
        }

        [HttpGet]
        public IActionResult GetExcuseById(int id)
        {
            Excuse excuseFromDb = _db.Excuses.SingleOrDefault(e => e.Id == id);

            if (excuseFromDb == null) return NotFound();
            return Ok(excuseFromDb);
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetExcuseById()
        {
            return Ok("super!");
        }
    }
}