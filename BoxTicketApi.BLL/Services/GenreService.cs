using BoxTicketApi.BLL.Responses.Genre;
using BoxTicketApi.BLL.Services.Base;
using BoxTicketApi.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.BLL.Services
{
    public class GenreService : IGenreService
    {
        private GenreRepository _genreRepository;
        public GenreService(GenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<List<GenreResponse>> GetAllGenre()
        {
            var genres = await _genreRepository.GetAllAsync();
            List<GenreResponse> responseList = new();

            foreach (var genre in genres)
            {
                GenreResponse response = new GenreResponse();
                response.Id = genre.Id;
                response.NameGenre = genre.NameGenre;

                responseList.Add(response);
            }
            return responseList;
        }
    }
}
