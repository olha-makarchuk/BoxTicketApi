using AutoMapper;
using BoxTicketApi.BLL.Requests.Genre;
using BoxTicketApi.BLL.Responses.Genre;
using BoxTicketApi.BLL.Services.Base;
using BoxTicketApi.DAL.Entities;
using BoxTicketApi.DAL.Repositories;
using BoxTicketApi.DAL.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.BLL.Services
{
    public class GenreService : IGenreService
    {
        private IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GenreService(IGenreRepository genreRepository, IMapper mapper)
        {
            _mapper = mapper;
            _genreRepository = genreRepository;
        }

        public async Task<GenreResponse> AddGenre(GenreRequest request)
        {
            var genre = _mapper.Map<Genre>(request);
            await _genreRepository.AddAsync(_mapper.Map<Genre>(request));

            return _mapper.Map<GenreResponse>(genre); ;
        }

        public async Task<List<GenreResponse>> GetAllGenre()
        {
            var genres = await _genreRepository.GetAllAsync();
            
            return _mapper.Map<List<GenreResponse>>(genres); ;
        }
    }
}
