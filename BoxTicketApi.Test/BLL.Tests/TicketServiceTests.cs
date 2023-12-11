using AutoMapper;
using Azure.Core;
using BoxTicketApi.BLL.Requests.Ticket;
using BoxTicketApi.BLL.Responses.Author;
using BoxTicketApi.BLL.Responses.Performance;
using BoxTicketApi.BLL.Responses.Ticket;
using BoxTicketApi.BLL.Services;
using BoxTicketApi.BLL.Services.Base;
using BoxTicketApi.DAL.Entities;
using BoxTicketApi.DAL.Repositories.Base;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.Test.BLL.Tests
{
    public class TicketServiceTests
    {
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<ITicketRepository> _ticketRepositoryMock = new();
        private TicketService _ticketService = null!;

        [Fact]
        public async Task BookTicket_WhenSeatAvailable_ReturnsTicketIdResponse()
        {
            TicketIdResponse response = new TicketIdResponse() { idTicket=1};
            Ticket ticket = new Ticket();
            var request = new TicketReqest{IdTicketOptions = 1,SeatNumber = 5,IdUser = 123};
            int idPerformance = 1; 
            var boughtSeats = new List<int> { 1, 3, 7 };

            _mapperMock.Setup(m => m.Map<TicketIdResponse>(It.IsAny<Ticket>()))
               .Returns(response);
            _ticketRepositoryMock.Setup(repo => repo.GetBoughtSeatsByType(idPerformance, request.IdTicketOptions))
                .ReturnsAsync(boughtSeats);
            _ticketRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Ticket>()))
                .Callback<Ticket>(ticket =>{ticket.Id = 1;})
                .Returns(Task.CompletedTask);
            _ticketService = new TicketService(_ticketRepositoryMock.Object, _mapperMock.Object);

            var result = await _ticketService.BookTicket(request, idPerformance);

            Assert.NotNull(result);
            Assert.Equal(1, result.idTicket);
        }

        [Fact]
        public async Task BookTicket_WhenSeatAlreadyBooked_ThrowsException()
        {
            TicketIdResponse response = new TicketIdResponse() { idTicket = 1 };
            var request = new TicketReqest { IdTicketOptions = 1, SeatNumber = 3, IdUser = 123 };
            int idPerformance = 1;
            var boughtSeats = new List<int> { 1, 3, 7 };

            _ticketRepositoryMock.Setup(repo => repo.GetBoughtSeatsByType(idPerformance, request.IdTicketOptions))
                .ReturnsAsync(boughtSeats);
            _mapperMock.Setup(m => m.Map<TicketIdResponse>(It.IsAny<Ticket>()))
                .Returns(response);
            _ticketService = new TicketService(_ticketRepositoryMock.Object, _mapperMock.Object);

            var exception = await Assert.ThrowsAsync<Exception>(async () => await _ticketService.BookTicket(request, idPerformance));

            Assert.Equal($"Квиток з місцем {request.SeatNumber} не доступний.", exception.Message);
        }

        [Fact]
        public async Task BuyTicket_WhenSeatAvailable_ReturnsTicketIdResponse()
        {
            TicketIdResponse response = new TicketIdResponse() { idTicket = 1 };
            Ticket ticket = new Ticket();
            var request = new TicketReqest { IdTicketOptions = 1, SeatNumber = 5, IdUser = 123 };
            int idPerformance = 1;
            var boughtSeats = new List<int> { 1, 3, 7 };

            _mapperMock.Setup(m => m.Map<TicketIdResponse>(It.IsAny<Ticket>()))
               .Returns(response);
            _ticketRepositoryMock.Setup(repo => repo.GetBoughtSeatsByType(idPerformance, request.IdTicketOptions))
                .ReturnsAsync(boughtSeats);
            _ticketRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Ticket>()))
                .Callback<Ticket>(ticket => { ticket.Id = 1; })
                .Returns(Task.CompletedTask);
            _ticketService = new TicketService(_ticketRepositoryMock.Object, _mapperMock.Object);

            var result = await _ticketService.BuyTicket(request, idPerformance);

            Assert.NotNull(result);
            Assert.Equal(1, result.idTicket);
        }

        [Fact]
        public async Task BuyTicket_WhenSeatAlreadyBooked_ThrowsException()
        {
            TicketIdResponse response = new TicketIdResponse() { idTicket = 1 };
            var request = new TicketReqest { IdTicketOptions = 1, SeatNumber = 3, IdUser = 123 };
            int idPerformance = 1;
            var boughtSeats = new List<int> { 1, 3, 7 };

            _ticketRepositoryMock.Setup(repo => repo.GetBoughtSeatsByType(idPerformance, request.IdTicketOptions))
                .ReturnsAsync(boughtSeats);
            _mapperMock.Setup(m => m.Map<TicketIdResponse>(It.IsAny<Ticket>()))
                .Returns(response);
            _ticketService = new TicketService(_ticketRepositoryMock.Object, _mapperMock.Object);

            var exception = await Assert.ThrowsAsync<Exception>(async () => await _ticketService.BuyTicket(request, idPerformance));

            Assert.Equal($"Квиток з місцем {request.SeatNumber} не доступний.", exception.Message);
        }

        [Fact]
        public async Task BuyBookedTicket_WhenTicketFoundAndBelongsToUser_ReturnsTicketIdResponse()
        {
            var request = new TicketByIdReqest{ Id = 1,IdUser = 1};
            var ticket = new Ticket{Id = 1,IdUser = request.IdUser};
            var ticketExpected = new TicketIdResponse { idTicket=1};

            _ticketRepositoryMock.Setup(repo => repo.GetByIdAsync(request.Id))
                .ReturnsAsync(ticket);
            _ticketRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Ticket>()))
                .Callback<Ticket>(ticketToUpdate =>
                {
                    ticketToUpdate.IdStatus = 1;  });
            _ticketService = new TicketService(_ticketRepositoryMock.Object, _mapperMock.Object);
            _mapperMock.Setup(m => m.Map<TicketIdResponse>(It.IsAny<Ticket>()))
                .Returns(ticketExpected);

            var result = await _ticketService.BuyBookedTicket(request);

            Assert.NotNull(result);
            Assert.Equal(request.Id, result.idTicket);
        }

        [Fact]
        public async Task BuyBookedTicket_WhenTicketNotFound_ThrowsException()
        {
            var request = new TicketByIdReqest
            {
                Id = 1,
                IdUser = 2 
            };

            _ticketRepositoryMock.Setup(repo => repo.GetByIdAsync(request.Id))
                .ReturnsAsync(() => null);
            _ticketService = new TicketService(_ticketRepositoryMock.Object, _mapperMock.Object);

            var exception = await Assert.ThrowsAsync<Exception>(async () => await _ticketService.BuyBookedTicket(request));

            Assert.Equal($"Квиток з id {request.Id} не знайдений.", exception.Message);
        }

        [Fact]
        public async Task BuyBookedTicket_WhenTicketBelongsToAnotherUser_ThrowsException()
        {
            var request = new TicketByIdReqest
            {
                Id = 1,
                IdUser = 1 
            };

            var ticket = new Ticket
            {
                Id = 1,
                IdUser = 2
            };

            _ticketRepositoryMock.Setup(repo => repo.GetByIdAsync(request.Id))
                .ReturnsAsync(ticket);
            _ticketService = new TicketService(_ticketRepositoryMock.Object, _mapperMock.Object);
            var exception = await Assert.ThrowsAsync<Exception>(async () => await _ticketService.BuyBookedTicket(request));

            Assert.Equal($"Квиток з id {request.Id} бронював інший користувач.", exception.Message);
        }


        [Fact]
        public async Task CancelBookedTicket_WhenTicketFoundAndBelongsToUser_ReturnsTicketIdResponse()
        {
            var request = new TicketByIdReqest { Id = 1, IdUser = 1 };
            var ticket = new Ticket { Id = 1, IdUser = request.IdUser };
            var ticketExpected = new TicketIdResponse { idTicket = 1 };

            _mapperMock.Setup(m => m.Map<TicketIdResponse>(It.IsAny<TicketByIdReqest>()))
                .Returns(ticketExpected);

            _ticketRepositoryMock.Setup(repo => repo.GetByIdAsync(request.Id))
                    .ReturnsAsync(ticket);

            _ticketService = new TicketService(_ticketRepositoryMock.Object, _mapperMock.Object);

            var result = await _ticketService.CancelBookedTicket(request);

            Assert.NotNull(result);
            Assert.Equal(request.Id, result.idTicket);
        }

        [Fact]
        public async Task CancelBookedTicket_WhenTicketNotFound_ThrowsException()
        {
            var request = new TicketByIdReqest
            {
                Id = 1, 
                IdUser = 123 
            };

            _ticketRepositoryMock.Setup(repo => repo.GetByIdAsync(request.Id))
                .ReturnsAsync(() => null); 

            _ticketService = new TicketService(_ticketRepositoryMock.Object, _mapperMock.Object);

            var exception = await Assert.ThrowsAsync<Exception>(async () => await _ticketService.CancelBookedTicket(request));

            Assert.Equal($"Квиток з id {request.Id} не знайдений.", exception.Message);
        }

        [Fact]
        public async Task CancelBookedTicket_WhenTicketBelongsToAnotherUser_ThrowsException()
        {
            var request = new TicketByIdReqest
            {
                Id = 1, 
                IdUser = 1
            };

            var ticket = new Ticket
            {
                Id = 1,
                IdUser = 2, 
            };

            _ticketRepositoryMock.Setup(repo => repo.GetByIdAsync(request.Id))
                .ReturnsAsync(ticket);

            _ticketService = new TicketService(_ticketRepositoryMock.Object, _mapperMock.Object);

            var exception = await Assert.ThrowsAsync<Exception>(async () => await _ticketService.CancelBookedTicket(request));

            Assert.Equal($"Квиток з id {request.Id} бронював інший користувач.", exception.Message);
        }

        [Fact]
        public async Task GetAllTickets_ReturnsListOfTickets()
        {
            int userId = 1; 
            var ticketsList = new List<Ticket>
            {
                new Ticket { Id = 1, IdUser = userId},
                new Ticket { Id = 2, IdUser = userId},
            };

            var ticketsListExpected = new List<TicketResponse>
            {
                new TicketResponse{ Id =1, IdUser=userId, IdPerformance=1, Price=100, SeatNumber=4, Status="Проданий", IdTicketOptions=1, Performance="p"},
                new TicketResponse{ Id =1, IdUser=userId, IdPerformance=2, Price=200, SeatNumber=1, Status="Проданий", IdTicketOptions=3, Performance="p"}
            };

            _ticketRepositoryMock.Setup(repo => repo.GetAllTicketsById(userId))
                .ReturnsAsync(ticketsList);
            _mapperMock.Setup(m => m.Map<List<TicketResponse>>(It.IsAny<List<Ticket>>()))
                .Returns(ticketsListExpected);
            _ticketService = new TicketService(_ticketRepositoryMock.Object, _mapperMock.Object);

            var result = await _ticketService.GetAllTickets(userId);

            Assert.NotNull(result);
            Assert.Equal(ticketsList.Count, result.Count); 
        }

    }
}
