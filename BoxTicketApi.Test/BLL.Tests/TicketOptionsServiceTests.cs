using AutoMapper;
using BoxTicketApi.BLL.Responses.TicketOptions;
using BoxTicketApi.BLL.Services;
using BoxTicketApi.DAL.Repositories.Base;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoxTicketApi.BLL.Requests.TicketOptions;
using BoxTicketApi.DAL.Entities;
using BoxTicketApi.BLL.Mapper;

namespace BoxTicketApi.Test.BLL.Tests
{
    public class TicketOptionsServiceTests
    {
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<ITicketOptionsRepository> _ticketOptionsRepositoryMock = new();
        private TicketOptionsService _ticketOptionsService = null!;

        [Fact]
        public async Task GetAllAvailableTickets_WhenTicketsFound_ShouldReturnsTicketOptions()
        {
            var request = new GetOptionsRequest { IdPerformance = 1 }; 
            var allTickets = new List<AllTicket>
            {
                new AllTicket { IdType = 1, CoutOfTickets = 10 }
            };

            var purchasedSeats = new List<int> { 1, 3, 5 }; 

            _ticketOptionsRepositoryMock.Setup(repo => repo.GetAllTickets(request.IdPerformance))
                .ReturnsAsync(allTickets);

            _ticketOptionsRepositoryMock.Setup(repo => repo.GetBoughtSeatsByType(request.IdPerformance, It.IsAny<int>()))
                .ReturnsAsync((int idPerformance, int idType) =>
                {
                    return idType == 1 ? purchasedSeats : new List<int>(); 
                });

            _mapperMock.Setup(mapper => mapper.Map<OptionsResponse>(It.IsAny<AllTicket>()))
                .Returns((AllTicket ticketOptions) =>
                {
                    return new OptionsResponse
                    {
                        Id = ticketOptions.IdType,
                        CoutOfTickets =30,
                        NamePerformance="PerformanceName",
                        Price =100,
                        TypeName ="type"
                    };
                });

            _ticketOptionsService = new TicketOptionsService(_ticketOptionsRepositoryMock.Object, _mapperMock.Object);

            var result = await _ticketOptionsService.GetAllAvailableTickets(request);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetAllAvailableTickets_WhenTicketsFound_ShouldReturnsExeption()
        {
            var request = new GetOptionsRequest { IdPerformance = 1 };

            List<AllTicket> allTickets = new List<AllTicket>();
            _ticketOptionsRepositoryMock.Setup(repo => repo.GetAllTickets(request.IdPerformance))
                .ReturnsAsync(allTickets);

            _ticketOptionsService = new TicketOptionsService(_ticketOptionsRepositoryMock.Object, _mapperMock.Object);

            var exception = await Assert.ThrowsAsync<Exception>(async () => await _ticketOptionsService.GetAllAvailableTickets(request));

            Assert.Equal($"Квитків з id={request.IdPerformance} не знайдено", exception.Message);
        }

        [Fact]
        public async Task GetIdPerformanceInOption_WhenOptionNotFound_ThrowsException()
        {
            int idOption = 1; 

            _ticketOptionsRepositoryMock.Setup(repo => repo.GetByIdAsync(idOption))
                .ReturnsAsync(() => null); 
            _ticketOptionsService = new TicketOptionsService(_ticketOptionsRepositoryMock.Object, _mapperMock.Object);

            var exception = await Assert.ThrowsAsync<Exception>(async () => await _ticketOptionsService.GetIdPerformanceInOption(idOption));

            Assert.Equal($"No Option with Id {idOption}", exception.Message);
        }

        [Fact]
        public async Task GetIdPerformanceInOption_WhenOptionFound_ReturnsIdPerformance()
        {
            int idOption = 1; 
            int expectedIdPerformance = 10; 

            var option = new AllTicket { Id = idOption, IdPerformance = expectedIdPerformance };

            _ticketOptionsRepositoryMock.Setup(repo => repo.GetByIdAsync(idOption))
                .ReturnsAsync(option);

            _ticketOptionsService = new TicketOptionsService(_ticketOptionsRepositoryMock.Object, _mapperMock.Object);

            var expectedidPerformance = await _ticketOptionsService.GetIdPerformanceInOption(idOption);

            Assert.Equal(expectedIdPerformance, expectedidPerformance);
        }
    }
}
