using BoxTicketApi.BLL.Requests.Performance;
using BoxTicketApi.BLL.Services;
using BoxTicketApi.BLL.Services.Base;
using Microsoft.AspNetCore.Mvc;

namespace BoxTicketApi.Controllers
{
    public class GenreController : Controller
    {
        public IGenreService _genreService;
        
        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpPost("AllGenre")]
        public async Task<IActionResult> GetAllGenre()
        {
            var result = await _genreService.GetAllGenre();

            return Ok(result);
        }
    }
}
