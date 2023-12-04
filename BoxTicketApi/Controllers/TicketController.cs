using Microsoft.AspNetCore.Mvc;

namespace BoxTicketApi.Controllers
{
    public class TicketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
