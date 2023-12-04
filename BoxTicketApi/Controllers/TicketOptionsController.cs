using Microsoft.AspNetCore.Mvc;

namespace BoxTicketApi.Controllers
{
    public class TicketOptionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
