using AutoMapper;
using BoxTicketApi.BLL.Requests.Performance;
using BoxTicketApi.BLL.Responses.Author;
using BoxTicketApi.BLL.Responses.Performance;
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
    public class PerformanceService : IPerformanceService
    {
        private readonly IMapper _mapper;
        private PerformanceRepository _performanceRepository;
        private AuthorRepository _authorRepository;
        private GenreRepository _genreRepository;

        public PerformanceService(IMapper mapper, PerformanceRepository repository, AuthorRepository authorRepository, GenreRepository genreRepository)
        {
            _mapper = mapper;
            _authorRepository = authorRepository;
            _genreRepository = genreRepository;
            _performanceRepository = repository;
        }

        public async Task<PerformanceResponse> AddPerformance(PerformanceRequest request)
        {
            var author = await _authorRepository.GetByIdAsync(request.IdAuthor);
            if(author == null )
            {
                throw new Exception($"Author with id {request.IdAuthor} not found");
            }

            var genre = await _genreRepository.GetByIdAsync(request.IdGenre);
            if (genre == null)
            {
                throw new Exception($"Genre with id {request.IdGenre} not found");
            }

            Performance performance = new() { IdAuthor = request.IdAuthor, DateTimeEvent = request.DateTimeEvent, IdGenre = request.IdGenre, PerformanceName = request.PerformanceName };
            await _performanceRepository.AddAsync(performance);

            return _mapper.Map<PerformanceResponse>(performance);
        }

        public async Task<List<PerformanceResponse>> GetAllPerformances()
        {
            var performances = await _performanceRepository.GetAllAsync();

            return _mapper.Map<List<PerformanceResponse>>(performances);
        }

        public async Task<List<PerformanceResponse>> GetPerformancesByAuthor(PerformancesByAuthorRequest request)
        {
            var performances = await _performanceRepository.GetPerformancesByAuthor(request.idAuthor);
            if(performances.Count == 0)
            {
                throw new Exception($"Performances with author id {request.idAuthor} not found");
            }

            return _mapper.Map<List<PerformanceResponse>>(performances);
        }

        public async Task<List<PerformanceResponse>> GetPerformancesByDate(PerformancesByDateRequest request)
        {
            var performances = await _performanceRepository.GetPerformancesByDate(request.dateTime);
            if (performances.Count == 0)
            {
                throw new Exception($"Performances with date {request.dateTime} not found");
            }

            return _mapper.Map<List<PerformanceResponse>>(performances);
        }

        public async Task<List<PerformanceResponse>> GetPerformancesByGenre(PerformancesByGenreRequest request)
        {
            var performances = await _performanceRepository.GetPerformancesByGenre(request.idGenre);
            if (performances.Count == 0)
            {
                throw new Exception($"Performances with genre id {request.idGenre} not found");
            }

            return _mapper.Map<List<PerformanceResponse>>(performances);
        }

        public async Task<List<PerformanceResponse>> GetPerformancesByName(PerformancesByNameRequest request)
        {
            var performances = await _performanceRepository.GetPerformancesByName(request.Name);
            if (performances.Count == 0)
            {
                throw new Exception($"Performances with name {request.Name} not found");
            }

            return _mapper.Map<List<PerformanceResponse>>(performances);
        }
    }
}