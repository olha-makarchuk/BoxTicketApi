using AutoMapper;
using BoxTicketApi.BLL.Requests.TicketOptions;
using BoxTicketApi.BLL.Responses.TicketOptions;
using BoxTicketApi.BLL.Services.Base;
using BoxTicketApi.DAL.Entities;
using BoxTicketApi.DAL.Repositories;
using BoxTicketApi.DAL.Repositories.Base;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.BLL.Services
{
    public class TicketOptionsService : ITicketOptionsService
    {
        private ITicketOptionsRepository _ticketOptionsRepository;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public TicketOptionsService(ITicketOptionsRepository ticketOptionsRepository, IConfiguration configuration, IMapper mapper)
        {
            _mapper = mapper;
            _config = configuration;
            _ticketOptionsRepository = ticketOptionsRepository;
        }

        public async Task<List<OptionsResponse>> GetAllAvailableTickets(GetOptionsRequest request)
        {
            var allTickets = await _ticketOptionsRepository.GetAllTickets(request.IdPerformance);
            if(allTickets.Count != 0)
            {
                List<OptionsResponse> responseList = new();

                foreach (var tickets in allTickets)
                {
                    var purchasedSeats = await _ticketOptionsRepository.GetBoughtSeatsByType(request.IdPerformance, tickets.IdType);

                    List<int> seats = new();
                    for (int i = 1; i <= tickets.CoutOfTickets; i++)
                    {
                        seats.Add(i);
                    }
                    foreach (var seat in purchasedSeats)
                    {
                        seats.Remove(seat);
                    }

                    var maper = _mapper.Map<OptionsResponse>(tickets);
                    maper.Seats = seats;

                    responseList.Add(maper);
                }
                return responseList;
            }
            else
            {
                throw new Exception($"Квитків з id={request.IdPerformance} не знайдено");
            }
        }

        public async Task<int> GetIdPerformanceInOption(int idOption)
        {
            var option =  await _ticketOptionsRepository.GetByIdAsync(idOption);

            return _mapper.Map<int>(option.IdPerformance);
        }
    }
}
