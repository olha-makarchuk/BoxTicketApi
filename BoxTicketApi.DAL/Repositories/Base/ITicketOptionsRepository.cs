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
        Task<List<AllTicket>> GetAllAvailableTicketTypes(int idPerformance);
        Task<List<AllTicket>> GetAllAvailableSeats(int idAllTicket);
    }
}
