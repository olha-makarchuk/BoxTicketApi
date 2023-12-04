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

        public async Task<List<PerformanceResponse>> GetAllPerformances()
        {
            var performances = await _repository.GetAllAsync();

            return CreateList(performances);
        }

        public async Task<List<PerformanceResponse>> GetPerformancesByAuthor(PerformancesByAuthorRequest request)
        {
            var performances = await _repository.GetPerformancesByAuthor(request.idAuthor);
            if(performances.Count == 0)
            {
                throw new Exception($"Performances with author id {request.idAuthor} not found");
            }

            return CreateList(performances);
        }

        public async Task<List<PerformanceResponse>> GetPerformancesByDate(PerformancesByDateRequest request)
        {
            var performances = await _repository.GetPerformancesByDate(request.dateTime);
            if (performances.Count == 0)
            {
                throw new Exception($"Performances with date {request.dateTime} not found");
            }

            return CreateList(performances);
        }

        public async Task<List<PerformanceResponse>> GetPerformancesByGenre(PerformancesByGenreRequest request)
        {
            var performances = await _repository.GetPerformancesByGenre(request.idGenre);
            if (performances.Count == 0)
            {
                throw new Exception($"Performances with genre id {request.idGenre} not found");
            }

            return CreateList(performances);
        }

        public async Task<List<PerformanceResponse>> GetPerformancesByName(PerformancesByNameRequest request)
        {
            var performances = await _repository.GetPerformancesByName(request.Name);
            if (performances.Count == 0)
            {
                throw new Exception($"Performances with name {request.Name} not found");
            }

            return CreateList(performances);
        }

        private List<PerformanceResponse> CreateList(List<Performance> performances)
        {
            List<PerformanceResponse> list = new();

            foreach (var p in performances)
            {
                PerformanceResponse responseItem = new PerformanceResponse();
                responseItem.IdAuthor = p.IdAuthor;
                responseItem.IdPerformance = p.Id;
                responseItem.IdGenre = p.IdGenre;
                responseItem.DateTimeEvent = p.DateTimeEvent;
                responseItem.PerformanceName = p.PerformanceName;
                list.Add(responseItem);
            }
            return list;
        }
    }
}