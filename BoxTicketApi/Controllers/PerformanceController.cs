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
            PerformancesByDateRequest request = new();
            request.Date = date;

            var result = await _performanceService.GetPerformancesByDate(request);

            return Ok(result);
        }

        [HttpPost("PerformancesByAuthor")]
        public async Task<IActionResult> GetPerformancesByAuthor(PerformancesByAuthorRequest request)
        {
            var result = await _performanceService.GetPerformancesByAuthor(request);

            return Ok(result);
        }

        [HttpPost("PerformancesByGenre")]
        public async Task<IActionResult> GetPerformancesByGenre(PerformancesByGenreRequest request)
        {
            var result = await _performanceService.GetPerformancesByGenre(request);

            return Ok(result);
        }

        [HttpPost("PerformancesByName")]
        public async Task<IActionResult> GetPerformancesByName(PerformancesByNameRequest request)
        {
            var result = await _performanceService.GetPerformancesByName(request);

            return Ok(result);
        }

        [HttpPost("AllPerformances")]
        public async Task<IActionResult> GetAllPerformances()
        {
            var result = await _performanceService.GetAllPerformances();
            return Ok(result);
        }

    }
}
