using BoxTicketApi.BLL.Requests.TicketOptions;
using BoxTicketApi.BLL.Services;
using BoxTicketApi.BLL.Services.Base;
using Microsoft.AspNetCore.Mvc;

namespace BoxTicketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketOptionsController : Controller
    {
        
        private ITicketOptionsService _ticketOptionsService;
        private readonly IConfiguration _config;

        public TicketOptionsController(ITicketOptionsService ticketOptionsService, IConfiguration configuration)
        {
            _ticketOptionsService = ticketOptionsService;
            _config = configuration;
        }
        
        [HttpPost("AvaillableTickets")]
        public async Task<IActionResult> GetAvaillableTickets(GetOptionsRequest request)
        {
            var result = await _ticketOptionsService.GetAllAvailableTickets(request);
            return Ok(result);
        }
    }
}
