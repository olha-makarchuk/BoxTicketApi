using BoxTicketApi.BLL.Requests.Ticket;
using BoxTicketApi.BLL.Services;
using BoxTicketApi.BLL.Services.Base;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BoxTicketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : Controller
    {
        private ITicketService _ticketService;
        private readonly IConfiguration _config;

        public TicketController(ITicketService ticketService, IConfiguration configuration) 
        {
            _ticketService = ticketService;
            _config = configuration;
        }

        [HttpPost("Book")]
        public async Task<IActionResult> BookTicket(TicketReqest request)
        {
            if (Request.Cookies.TryGetValue("UserId", out string userId))
            {
                request.IdUser = Convert.ToInt32(userId);

                var result = await _ticketService.BookTicket(request);
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPost("Buy")]
        public async Task<IActionResult> BuyTicket(TicketReqest request)
        {
            if (Request.Cookies.TryGetValue("UserId", out string userId))
            {
                request.IdUser = Convert.ToInt32(userId);

                var result = await _ticketService.BookTicket(request);
                return Ok(result);
            }
            return BadRequest();
        }


        [HttpPost("BuyBooked")]
        public async Task<IActionResult> BuyBookedTicket(TicketByIdReqest request)
        {
            if (Request.Cookies.TryGetValue("UserId", out string userId))
            {
                request.IdUser = Convert.ToInt32(userId);

                var result = await _ticketService.BuyBookedTicket(request);
                return Ok(result);
            }
            return BadRequest();
        }


        [HttpGet("MyTickets")]
        public async Task<IActionResult> GetMyTickets()
        {
            if (Request.Cookies.TryGetValue("UserId", out string userId))
            {
                var result = await _ticketService.GetAllTickets(Convert.ToInt32(userId));
                return Ok(result);
            }
            return BadRequest();
        }

    }
}
