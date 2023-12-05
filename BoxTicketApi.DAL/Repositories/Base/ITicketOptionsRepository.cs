using BoxTicketApi.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.DAL.Repositories.Base
{
    public interface ITicketOptionsRepository : IGenericRepository<AllTicket>
    {
        Task<List<AllTicket>> GetAllTickets(int performanceId);
        Task<List<int>> GetBoughtSeatsByType(int performanceId, int idOption);
    }
}
