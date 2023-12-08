using BoxTicketApi.BLL.Requests.Ticket;
using BoxTicketApi.BLL.Services;
using BoxTicketApi.BLL.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BoxTicketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : Controller
    {
        private ITicketService _ticketService;
        private ITicketOptionsService _ticketOptionsService;
        private readonly IConfiguration _config;

        public TicketController(ITicketService ticketService, IConfiguration configuration, ITicketOptionsService ticketOptionsService) 
        {
            _ticketOptionsService = ticketOptionsService;
            _ticketService = ticketService;
            _config = configuration;
        }

        [HttpPost("Book"), Authorize(Roles = "User")]
        public async Task<IActionResult> BookTicket(TicketReqest request)
        {
            if (Request.Cookies.TryGetValue("UserId", out string userId))
            {
                request.IdUser = Convert.ToInt32(userId);
                
                var idPer =  await _ticketOptionsService.GetIdPerformanceInOption(request.IdTicketOptions);
                if (idPer != 0)
                {
                    var result = await _ticketService.BookTicket(request, idPer);
                    return Ok(result);
                }
            }
            throw new Exception($"Tcket with TicketOption id={request.IdTicketOptions} not found.");
        }

        [HttpPost("Buy"), Authorize(Roles = "User")]
        public async Task<IActionResult> BuyTicket(TicketReqest request)
        {
            if (Request.Cookies.TryGetValue("UserId", out string userId))
            {
                request.IdUser = Convert.ToInt32(userId);

                var idPer = await _ticketOptionsService.GetIdPerformanceInOption(request.IdTicketOptions);
                if (idPer != 0)
                {
                    var result = await _ticketService.BuyTicket(request, idPer);
                    return Ok(result);
                }
            }
            return BadRequest();
        }


        [HttpPost("BuyBooked"), Authorize(Roles = "User")]
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


        [HttpGet("MyTickets"), Authorize(Roles = "User")]
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
