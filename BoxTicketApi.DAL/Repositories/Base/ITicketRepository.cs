using BoxTicketApi.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.DAL.Repositories.Base
{
    public interface ITicketRepository : IGenericRepository<Ticket>
    {
        Task<List<int>> GetBoughtSeatsByType(int performanceId, int idOption);
        Task<List<Ticket>> GetAllTicketsById(int IdUser);
    }
}
