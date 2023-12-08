using BoxTicketApi.BLL.Requests.Author;
using BoxTicketApi.BLL.Services;
using BoxTicketApi.BLL.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BoxTicketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAuthor()
        {
            var result = await _authorService.GetAllAuthor();

            return Ok(result);
        }

        [HttpPost("Add"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAuthor( AuthorRequest request)
        {
            var result = await _authorService.AddAuthor(request);

            return Ok(result);
        }
    }
}
