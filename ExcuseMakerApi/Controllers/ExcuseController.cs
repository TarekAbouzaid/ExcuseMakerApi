using DTO;
using Microsoft.AspNetCore.Mvc;
using Models;
using Polly;
using Polly.Retry;
using Polly.Timeout;
using Services.Interfaces;

namespace ExcuseMakerApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class ExcuseController : ControllerBase
    {
        private readonly IExcuseService _service;
        private readonly AsyncRetryPolicy _retryPolicy;
        private readonly AsyncTimeoutPolicy _timeoutPolicy;

        public ExcuseController(IExcuseService service)
        {
            _service = service;

            // Retry policy with exponential backoff
            _retryPolicy = Policy
                .Handle<HttpRequestException>()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            // Timeout policy after 5 seconds
            _timeoutPolicy = Policy.TimeoutAsync(TimeSpan.FromSeconds(5));
        }

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
            return deleted ? Ok($"deleted ({id})") : NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateExcuse(Excuse excuse)
        {
            var updated = await _service.UpdateExcuse(excuse);
            return updated ? Ok($"updated ({excuse.Id})") : NotFound();
        }
        
        [HttpGet]
        [Route("GetRandomExcuse")]
        public async Task<IActionResult> GetRandomExcuse(ExcuseCategory category)
        {
            try
            {
                var policyWrap = Policy.WrapAsync(_retryPolicy, _timeoutPolicy);

                var randomExcuse = await policyWrap.ExecuteAsync(async ct =>
                {
                    return await _service.GetRandomExcuse(category);
                }, CancellationToken.None);

                if (randomExcuse != null)
                {
                    return Ok(randomExcuse.Text);
                }
                else
                {
                    return NotFound(); // Handle not found scenario appropriately
                }
            }
            catch (TimeoutRejectedException)
            {
                return StatusCode(500, "Request timed out");
            }
            catch (Exception ex)
            {
                // Log or handle other exceptions
                return StatusCode(500, "An error occurred while fetching the random excuse.");
            }
        }
    }
}