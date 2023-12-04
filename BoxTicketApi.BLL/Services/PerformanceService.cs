using BoxTicketApi.BLL.Requests.Performance;
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
        private PerformanceRepository _repository;

        public PerformanceService(PerformanceRepository repository)
        {
            _repository = repository;
        }

        public Task<PerformanceResponse> GetAllPerformances()
        {
            throw new NotImplementedException();
        }

        public Task<List<Performance>> GetPerformancesByDate(PerformancesByDateRequest request)
        {
            DateOnly dateOnly = new(re)
            var performances = _repository.GetPerformancesByDate(request.dateTime);
            return performances;
        }

        public Task<Performance> GetPerformancesByGenre(PerformancesByGenreRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<PerformanceResponse> GetPerformancesByName(PerformancesByNameRequest request)
        {
            throw new NotImplementedException();
        }

        Task<PerformanceResponse> IPerformanceService.GetPerformancesByAuthor(PerformancesByAuthorRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
