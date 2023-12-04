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
    public class TicketsRepository : GenericRepository<Ticket>, ITicketRepository
    {
        public TicketsRepository(BoxTicketContext context) : base(context)
        {
        }

        public Task<Ticket> BookTicket(int idUser, int idAllTicket, int seatNumber)
        {

            throw new NotImplementedException();
        }

        public Task<Ticket> BuyBookedTicket(int idTicket)
        {
            throw new NotImplementedException();
        }

        public Task<Ticket> BuyTicket(int idUser, int idAllTicket, int seatNumber)
        {
            throw new NotImplementedException();
        }

        public Task<Ticket> CancelBookedTicket(int idTicket)
        {
            throw new NotImplementedException();
        }
    }
}
