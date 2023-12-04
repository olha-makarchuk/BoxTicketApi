using BoxTicketApi.BLL.Requests.Ticket;
using BoxTicketApi.BLL.Responses.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.BLL.Services.Base
{
    public interface ITicketService
    {
        Task<TicketResponse> BuyTicket(TicketReqest reqest);
        Task<TicketResponse> BookTicket(TicketReqest reqest);
        Task<TicketResponse> BuyBookedTicket(TicketByIdReqest reqest);
        Task<TicketResponse> GetTicketById(TicketByIdReqest reqest);
        Task<TicketResponse> CancelBookedTicket(TicketByIdReqest reqest);
    }
}
