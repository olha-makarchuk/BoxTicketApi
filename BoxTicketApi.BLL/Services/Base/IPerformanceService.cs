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
        Task<PerformanceResponse> GetAllPerformances();
        Task<PerformanceResponse> GetPerformancesByAuthor(PerformancesByAuthorRequest request);
        Task<Performance> GetPerformancesByGenre(PerformancesByGenreRequest request);
        Task<PerformanceResponse> GetPerformancesByName(PerformancesByNameRequest request);
        Task<List<Performance>> GetPerformancesByDate(PerformancesByDateRequest request);
    }
}
