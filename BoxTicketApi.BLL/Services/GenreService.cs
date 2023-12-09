using AutoMapper;
using BoxTicketApi.BLL.Requests.Genre;
using BoxTicketApi.BLL.Responses.Genre;
using BoxTicketApi.BLL.Services.Base;
using BoxTicketApi.DAL.Entities;
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
        private readonly IMapper _mapper;

        public GenreService(GenreRepository genreRepository, IMapper mapper)
        {
            _mapper = mapper;
            _genreRepository = genreRepository;
        }

        public async Task<GenreResponse> AddGenre(GenreRequest request)
        {
            Genre genre= new() { NameGenre = request.Name};
            await _genreRepository.AddAsync(genre);

            return _mapper.Map<GenreResponse>(genre); ;
        }

        public async Task<List<GenreResponse>> GetAllGenre()
        {
            var genres = await _genreRepository.GetAllAsync();
            
            return _mapper.Map<List<GenreResponse>>(genres); ;
        }
    }
}
