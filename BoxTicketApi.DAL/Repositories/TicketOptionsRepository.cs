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
    public class TicketOptionsRepository : GenericRepository<AllTicket>, ITicketOptionsRepository
    {
        public TicketOptionsRepository(BoxTicketContext context) : base(context)
        {
        }

        public Task<List<AllTicket>> GetAllAvailableSeats(int idAllTicket)
        {
            throw new NotImplementedException();
        }

        public Task<List<AllTicket>> GetAllAvailableTicketTypes(int idPerformance)
        {
            throw new NotImplementedException();
        }
    }
}
