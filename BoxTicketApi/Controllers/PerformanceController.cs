using BoxTicketApi.BLL.Requests.Auth;
using BoxTicketApi.BLL.Requests.Performance;
using BoxTicketApi.BLL.Services;
using BoxTicketApi.BLL.Services.Base;
using Microsoft.AspNetCore.Mvc;

namespace BoxTicketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerformanceController : Controller
    {
        private readonly IPerformanceService _performanceService;
        private readonly IConfiguration _configuration;

        public PerformanceController(IConfiguration configuration, IPerformanceService performanceService)
        {
            _configuration = configuration;
            _performanceService = performanceService;
        }

        [HttpPost("PerformancesByDate")]
        public async Task<IActionResult> GetPerformancesByDate(string date)//рік-день-місяць
        {
            try
            {
                PerformancesByDateRequest request = new();
                request.Date = date;

                var result = await _performanceService.GetPerformancesByDate(request.dateTime);

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("PerformancesByAuthor")]
        public async Task<IActionResult> GetPerformancesByAuthor(PerformancesByAuthorRequest request)
        {
            return Ok();
        }

        [HttpPost("PerformancesByGenre")]
        public async Task<IActionResult> GetPerformancesByGenre(PerformancesByGenreRequest request)
        {
            return Ok();
        }

        [HttpPost("PerformancesByName")]
        public async Task<IActionResult> GetPerformancesByName(PerformancesByNameRequest request)
        {
            return Ok();
        }

        [HttpPost("AllPerformances")]
        public async Task<IActionResult> GetAllPerformances(PerformancesByNameRequest request)
        {
            return Ok();
        }
    }
}
