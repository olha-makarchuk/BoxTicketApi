using BoxTicketApi.DAL.Contexts;
using BoxTicketApi.DAL.Entities;
using BoxTicketApi.DAL.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.DAL.Repositories
{
    public class PerformanceRepository : GenericRepository<Performance>, IPerformanceRepository
    {
        public PerformanceRepository(BoxTicketContext context) : base(context)
        {
        }

        public Task<List<Performance>> GetPerformanceByAuthor(int idAuthor)
        {
            throw new NotImplementedException();
        }

        public Task<List<Performance>> GetPerformanceByDate(DateOnly date)
        {
            throw new NotImplementedException();
        }

        public Task<List<Performance>> GetPerformanceByGenre(int idGenre)
        {
            throw new NotImplementedException();
        }

        public Task<List<Performance>> GetPerformanceByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
