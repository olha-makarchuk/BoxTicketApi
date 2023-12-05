using BoxTicketApi.BLL.Services;
using BoxTicketApi.BLL.Services.Base;
using Microsoft.AspNetCore.Mvc;

namespace BoxTicketApi.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpPost("AllAuthor")]
        public async Task<IActionResult> GetAllAuthor()
        {
            var result = await _authorService.GetAllAuthor();

            return Ok(result);
        }
    }
}
