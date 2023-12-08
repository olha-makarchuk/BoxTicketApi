using BoxTicketApi.BLL.Requests.Genre;
using BoxTicketApi.BLL.Requests.Performance;
using BoxTicketApi.BLL.Services;
using BoxTicketApi.BLL.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BoxTicketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : Controller
    {
        public IGenreService _genreService;
        
        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllGenre()
        {
            var result = await _genreService.GetAllGenre();

            return Ok(result);
        }

        [HttpPost("Add"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddGenre(GenreRequest request)
        {
            var result = await _genreService.AddGenre(request);

            return Ok(result);
        }
    }
}
