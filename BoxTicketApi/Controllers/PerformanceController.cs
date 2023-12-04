using BoxTicketApi.BLL.Requests.Auth;
using BoxTicketApi.BLL.Requests.Performance;
using BoxTicketApi.BLL.Services;
using BoxTicketApi.BLL.Services.Base;
using BoxTicketApi.DAL.Contexts;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

                var result = await _performanceService.GetPerformancesByDate(request);

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
            try
            {
                var result = await _performanceService.GetPerformancesByAuthor(request);

                return Ok(result);
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("PerformancesByGenre")]
        public async Task<IActionResult> GetPerformancesByGenre(PerformancesByGenreRequest request)
        {
            try
            {
                var result = await _performanceService.GetPerformancesByGenre(request);

                return Ok(result);
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("PerformancesByName")]
        public async Task<IActionResult> GetPerformancesByName(PerformancesByNameRequest request)
        {
            try
            {
                var result = await _performanceService.GetPerformancesByName(request);

                return Ok(result);
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("AllPerformances")]
        public async Task<IActionResult> GetAllPerformances()
        {
            var result = await _performanceService.GetAllPerformances();
            return Ok(result);
        }

    }
}
