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

        public TicketOptionsController(ITicketOptionsService ticketOptionsService)
        {
            _ticketOptionsService = ticketOptionsService;
        }
        
        [HttpPost("AvaillableTickets")]
        public async Task<IActionResult> GetAvaillableTickets(GetOptionsRequest request)
        {
            var result = await _ticketOptionsService.GetAllAvailableTickets(request);
            return Ok(result);
        }
    }
}
