using BoxTicketApi.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoxTicketApi.BLL.Requests.Ticket;

namespace BoxTicketApi.DAL.Repositories.Base
{
    public interface ITicketRepository : IGenericRepository<Ticket>
    {
        Task<Ticket> BuyTicket(int idUser, int idAllTicket, int seatNumber);
        Task<Ticket> BookTicket(int idUser, int idAllTicket, int seatNumber);
        Task<Ticket> BuyBookedTicket(int idTicket);
        Task<Ticket> CancelBookedTicket(int idTicket);
    }
}
