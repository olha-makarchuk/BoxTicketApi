using AutoMapper;
using Azure.Core;
using BoxTicketApi.BLL.Requests.Ticket;
using BoxTicketApi.BLL.Responses.Ticket;
using BoxTicketApi.BLL.Services.Base;
using BoxTicketApi.DAL.Entities;
using BoxTicketApi.DAL.Repositories.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.BLL.Services
{
    public class TicketService : ITicketService
    {
        private ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;

        public TicketService(ITicketRepository ticketRepository, IMapper mapper)
        {
            _mapper = mapper;
            _ticketRepository = ticketRepository;
        }

        public async Task<TicketIdResponse> BookTicket(TicketReqest reqest, int IdPerformance)
        { 
            var boughtTicket = await _ticketRepository.GetBoughtSeatsByType(IdPerformance, reqest.IdTicketOptions);
            
            if(!boughtTicket.Contains(reqest.SeatNumber))
            {
                Ticket ticket = new Ticket();
                ticket.IdAllTickets = reqest.IdTicketOptions;
                ticket.SeatNumber = reqest.SeatNumber;
                ticket.IdUser = reqest.IdUser;
                ticket.IdStatus = 2;
                await _ticketRepository.AddAsync(ticket);

                return _mapper.Map<TicketIdResponse>(ticket);
            }
            else
            {
                throw new Exception($"Квиток з місцем {reqest.SeatNumber} не доступний.");
            }
        }

        public async Task<TicketIdResponse> BuyTicket(TicketReqest reqest, int IdPerformance)
        {
            var boughtTicket = await _ticketRepository.GetBoughtSeatsByType(IdPerformance, reqest.IdTicketOptions);

            if (!boughtTicket.Contains(reqest.SeatNumber))
            {
                Ticket ticket = new Ticket();
                ticket.IdAllTickets = reqest.IdTicketOptions;
                ticket.SeatNumber = reqest.SeatNumber;
                ticket.IdUser = reqest.IdUser;
                ticket.IdStatus = 1;

                await _ticketRepository.AddAsync(ticket);

                return _mapper.Map<TicketIdResponse>(ticket);
            }
            else
            {
                throw new Exception($"Квиток з місцем {reqest.SeatNumber} не доступний.");
            }
        }

        public async Task<TicketIdResponse> BuyBookedTicket(TicketByIdReqest reqest)
        {
            var ticket = await _ticketRepository.GetByIdAsync(reqest.Id);

            if (ticket != null)
            {
                if (ticket.IdUser == reqest.IdUser)
                {
                    var ticketNew = await _ticketRepository.GetByIdAsync(reqest.Id);
                    ticketNew.IdStatus = 1;
                    await _ticketRepository.UpdateAsync(ticketNew);

                    return _mapper.Map<TicketIdResponse>(ticketNew);
                }
                else
                {
                    throw new Exception($"Квиток з id {reqest.Id} бронював інший користувач.");
                }
            }
            else
            {
                throw new Exception($"Квиток з id {reqest.Id} не знайдений.");
            }
        }


        public async Task<TicketIdResponse> CancelBookedTicket(TicketByIdReqest reqest)
        {
            var ticket = await _ticketRepository.GetByIdAsync(reqest.Id);

            if (ticket != null)
            {
                if (ticket.IdUser == reqest.IdUser)
                {
                    await _ticketRepository.DeleteAsync(reqest.Id);
                    return _mapper.Map<TicketIdResponse>(reqest);
                }
                else
                {
                    throw new Exception($"Квиток з id {reqest.Id} бронював інший користувач.");
                }
            }
            else
            {
                throw new Exception($"Квиток з id {reqest.Id} не знайдений.");
            }
        }

        public async Task<List<TicketResponse>> GetAllTickets(int idUser)
        {
            var tickets = await _ticketRepository.GetAllTicketsById(idUser);

            return _mapper.Map<List<TicketResponse>>(tickets);
        }
    }
}
