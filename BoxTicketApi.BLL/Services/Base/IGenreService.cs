using BoxTicketApi.BLL.Requests.Genre;
using BoxTicketApi.BLL.Responses.Genre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.BLL.Services.Base
{
    public interface IGenreService
    {
        Task<List<GenreResponse>> GetAllGenre();
        Task<GenreResponse> AddGenre(GenreRequest request);
    }
}
