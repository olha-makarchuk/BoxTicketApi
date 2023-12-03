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
        Task<List<Performance>> GetPerformanceByAuthor(int idAuthor);
        Task<List<Performance>> GetPerformanceByGenre(int idGenre);
        Task<List<Performance>> GetPerformanceByName(string name);
        Task<List<Performance>> GetPerformanceByDate(DateOnly date);
    }
}
