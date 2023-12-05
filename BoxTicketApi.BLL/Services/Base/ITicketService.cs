using BoxTicketApi.BLL.Requests.Ticket;
using BoxTicketApi.BLL.Responses.Ticket;
using BoxTicketApi.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.BLL.Services.Base
{
    public interface ITicketService
    {
        Task<TicketIdResponse> BuyTicket(TicketReqest reqest);
        Task<TicketIdResponse> BookTicket(TicketReqest reqest);
        Task<TicketIdResponse> BuyBookedTicket(TicketByIdReqest reqest);
        Task<List<TicketResponse>> GetAllTickets(int idUser);
        Task<TicketIdResponse> CancelBookedTicket(TicketByIdReqest reqest);
    }
}
