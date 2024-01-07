using DTO;
using Microsoft.AspNetCore.Mvc;
using Models;
using Polly;
using Polly.CircuitBreaker;
using Polly.Extensions.Http;
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
        private readonly AsyncCircuitBreakerPolicy _circuitBreaker;

        public ExcuseController(IExcuseService service)
        {
            _service = service;

            // Retry policy with exponential backoff
            _retryPolicy = Policy
                .Handle<HttpRequestException>()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            // Timeout policy after 5 seconds
            _timeoutPolicy = Policy.TimeoutAsync(TimeSpan.FromSeconds(5));

            _circuitBreaker = Policy.Handle<HttpRequestException>()
                .CircuitBreakerAsync(
                    exceptionsAllowedBeforeBreaking: 2,
                    durationOfBreak: TimeSpan.FromMinutes(1)
                );

        }

        /// <summary>
        /// Creates an excuse and adds to service
        /// </summary>
        /// <param name="excuse"></param>
        [HttpPost] //Create Excuse
        public async Task<IActionResult> CreateExcuse(Excuse? excuse)
        {
            return await _circuitBreaker.ExecuteAsync(async () =>
            {
                var added = await _service.Add(excuse);
                return added
                    ? CreatedAtAction("GetExcuseById", new { id = excuse.Id }, excuse)
                    : StatusCode(400, "Something went wrong");
            });
        }

        /// <summary>
        /// Gets an excuse by ID
        /// </summary>
        /// <param name="excuse"></param>
        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetExcuseById(int id)
        {
            return await _circuitBreaker.ExecuteAsync(async () =>
            {
                var excuseFromDb = await _service.GetExcuseById(id);
                return Ok(excuseFromDb);
            });

        }

        /// <summary>
        /// Gets an excuse by category
        /// </summary>
        /// <param name="excuse"></param>
        [HttpGet]
        [Route("GetByCategory")]
        public async Task<IActionResult> GetExcuseByCategory(ExcuseCategory category)
        {
            return await _circuitBreaker.ExecuteAsync(async () =>
            {
                var excusesFromDb = await _service.GetExcusesByCategory(category);
                return Ok(excusesFromDb);
            });
        }

        /// <summary>
        /// Gets all excuses
        /// </summary>
        /// <param name="excuse"></param>
        [HttpGet]
        [Route("GetAllExcuses")]
        public async Task<IActionResult> GetAllExcuses()
        {
            return await _circuitBreaker.ExecuteAsync(async () =>
            {
                var excusesFromDb = await _service.GetAllExcuses();
                return Ok(excusesFromDb);
            });
        }

        /// <summary>
        /// Delete an excuse by ID
        /// </summary>
        /// <param name="excuse"></param>
        [HttpDelete]
        public async Task<IActionResult> DeleteExcuseById(int id)
        {
            return await _circuitBreaker.ExecuteAsync(async () =>
            {
                var deleted = await _service.DeleteExcuse(id);
                return deleted ? Ok($"deleted ({id})") : NotFound() as IActionResult;
            });
        }

        /// <summary>
        /// Update an excuse
        /// </summary>
        /// <param name="excuse"></param>
        [HttpPut]
        public async Task<IActionResult> UpdateExcuse(Excuse excuse)
        {
            return await _circuitBreaker.ExecuteAsync(async () =>
            {
                var updated = await _service.UpdateExcuse(excuse);
                return updated ? Ok($"updated ({excuse.Id})") as IActionResult : NotFound() as IActionResult;
            });
        }
        
        /// <summary>
        /// Get a random excuse
        /// </summary>
        /// <param name="excuse"></param>
        [HttpGet]
        [Route("GetRandomExcuse")]
        public async Task<IActionResult> GetRandomExcuse(ExcuseCategory category)
        {
            return await _circuitBreaker.ExecuteAsync(async () =>
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
                        return Ok(randomExcuse.Text) as IActionResult;
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
            });
        }
    }
}