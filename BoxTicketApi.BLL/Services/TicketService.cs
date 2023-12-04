using BoxTicketApi.BLL.Requests.Ticket;
using BoxTicketApi.BLL.Responses.Ticket;
using BoxTicketApi.BLL.Services.Base;
using BoxTicketApi.DAL.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.BLL.Services
{
    public class TicketService : ITicketService
    {
        private ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public Task<TicketResponse> BookTicket(TicketReqest reqest)
        {
            _ticketRepository.BookTicket(reqest)
        }

        public Task<TicketResponse> BuyBookedTicket(TicketByIdReqest reqest)
        {
            throw new NotImplementedException();
        }

        public Task<TicketResponse> BuyTicket(TicketReqest reqest)
        {
            throw new NotImplementedException();
        }

        public Task<TicketResponse> CancelBookedTicket(TicketByIdReqest reqest)
        {
            throw new NotImplementedException();
        }

        public Task<TicketResponse> GetTicketById(TicketByIdReqest reqest)
        {
            throw new NotImplementedException();
        }
    }
}
