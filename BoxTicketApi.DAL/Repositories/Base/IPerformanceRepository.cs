using BoxTicketApi.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.DAL.Repositories.Base
{
    public interface IPerformanceRepository : IGenericRepository<Performance>
    {
        Task<List<Performance>> GetPerformancesByAuthor(int idAuthor);
        Task<List<Performance>> GetPerformancesByGenre(int idGenre);
        Task<List<Performance>> GetPerformancesByName(string name);
        Task<List<Performance>> GetPerformancesByDate(DateOnly date);
    }
}
