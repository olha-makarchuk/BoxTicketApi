using BoxTicketApi.BLL.Requests.Performance;
using BoxTicketApi.BLL.Responses.Performance;
using BoxTicketApi.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.BLL.Services.Base
{
    public interface IPerformanceService
    {
        Task<List<PerformanceResponse>> GetAllPerformances();
        Task<List<PerformanceResponse>> GetPerformancesByAuthor(PerformancesByAuthorRequest request);
        Task<List<PerformanceResponse>> GetPerformancesByGenre(PerformancesByGenreRequest request);
        Task<List<PerformanceResponse>> GetPerformancesByName(PerformancesByNameRequest request);
        Task<List<PerformanceResponse>> GetPerformancesByDate(PerformancesByDateRequest request);
        Task<PerformanceResponse> AddPerformance(PerformanceRequest request);
    }
}
