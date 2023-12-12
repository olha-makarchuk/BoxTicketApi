using BoxTicketApi.BLL.Requests.Author;
using BoxTicketApi.BLL.Requests.Ticket;
using BoxTicketApi.BLL.Responses.Author;
using BoxTicketApi.BLL.Responses.Ticket;
using BoxTicketApi.BLL.Services;
using BoxTicketApi.BLL.Services.Base;
using BoxTicketApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.Test.Project.Test
{
    public class TicketControllerTests
    {
        private readonly Mock<ITicketService> _ticketServiceMock = new();
        private readonly Mock<ITicketOptionsService> _ticketOptionService = new(); 
        private TicketController _ticketController = null!;

        [Fact]
        public async Task BookTicket_ReturnTicketResponse()
        {
            var request = new TicketReqest()
            {
                IdTicketOptions=1,
                IdUser=1,
                SeatNumber=3
            };
            TicketIdResponse response = new()
            {
                idTicket = 1
            };

            _ticketOptionService.Setup(ds => ds.GetIdPerformanceInOption(request.IdTicketOptions))
                .ReturnsAsync(1);
            _ticketServiceMock.Setup(ds => ds.BookTicket(request, 1))
                .ReturnsAsync(response);
            _ticketController = new TicketController(_ticketServiceMock.Object, _ticketOptionService.Object);
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Cookie"] = $"UserId={request.IdUser}; UserId={request.IdUser}";
            _ticketController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            var returnedResult = await _ticketController.BookTicket(request);

            var Result = Assert.IsType<OkObjectResult>(returnedResult);
            Assert.NotNull(Result.Value);
        }
       
        [Fact]
        public async Task BookTicket_ReturnExeption()
        {
            var request = new TicketReqest()
            {
                IdTicketOptions = 1,
                IdUser = 1,
                SeatNumber = 3
            };
            TicketIdResponse response = new()
            {
                idTicket = 1
            };

            _ticketOptionService.Setup(ds => ds.GetIdPerformanceInOption(request.IdTicketOptions))
                .ReturnsAsync(0);
            _ticketServiceMock.Setup(ds => ds.BookTicket(request, 1))
                .ReturnsAsync(response);
            _ticketController = new TicketController(_ticketServiceMock.Object, _ticketOptionService.Object);
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Cookie"] = $"UserId={request.IdUser}; UserId={request.IdUser}";
            _ticketController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            var exception = await Assert.ThrowsAsync<Exception>(async () => await _ticketController.BookTicket(request));

            Assert.Equal($"Tcket with TicketOption id={request.IdTicketOptions} not found.", exception.Message);
        }

        [Fact]
        public async Task BuyTicket_ReturnTicketResponse()
        {
            var request = new TicketReqest()
            {
                IdTicketOptions = 1,
                IdUser = 1,
                SeatNumber = 3
            };
            TicketIdResponse response = new()
            {
                idTicket = 1
            };

            _ticketOptionService.Setup(ds => ds.GetIdPerformanceInOption(request.IdTicketOptions))
                .ReturnsAsync(1);
            _ticketServiceMock.Setup(ds => ds.BuyTicket(request, 1))
                .ReturnsAsync(response);
            _ticketController = new TicketController(_ticketServiceMock.Object, _ticketOptionService.Object);
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Cookie"] = $"UserId={request.IdUser}; UserId={request.IdUser}";
            _ticketController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            var returnedResult = await _ticketController.BuyTicket(request);

            var Result = Assert.IsType<OkObjectResult>(returnedResult);
            Assert.NotNull(Result.Value);
        }

        [Fact]
        public async Task BuyTicket_ReturnExeption()
        {
            var request = new TicketReqest()
            {
                IdTicketOptions = 1,
                IdUser = 1,
                SeatNumber = 3
            };
            TicketIdResponse response = new()
            {
                idTicket = 1
            };

            _ticketOptionService.Setup(ds => ds.GetIdPerformanceInOption(request.IdTicketOptions))
                .ReturnsAsync(0);
            _ticketServiceMock.Setup(ds => ds.BuyTicket(request, 1))
                .ReturnsAsync(response);
            _ticketController = new TicketController(_ticketServiceMock.Object, _ticketOptionService.Object);
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Cookie"] = $"UserId={request.IdUser}; UserId={request.IdUser}";
            _ticketController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            var exception = await Assert.ThrowsAsync<Exception>(async () => await _ticketController.BuyTicket(request));

            Assert.Equal($"Ticket with TicketOption id={request.IdTicketOptions} not found.", exception.Message);
        }

        [Fact]
        public async Task BuyBookedTicket_ReturnTicketResponse()
        {
            var request = new TicketByIdReqest()
            {
                Id=1,
                IdUser=1
            };
            TicketIdResponse response = new()
            {
                idTicket = 1
            };

            _ticketServiceMock.Setup(ds => ds.BuyBookedTicket(request))
                .ReturnsAsync(response);
            _ticketController = new TicketController(_ticketServiceMock.Object, _ticketOptionService.Object);
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Cookie"] = $"UserId={request.IdUser}; UserId={request.IdUser}";
            _ticketController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            var returnedResult = await _ticketController.BuyBookedTicket(request);

            var Result = Assert.IsType<OkObjectResult>(returnedResult);
            Assert.NotNull(Result.Value);
        }

        [Fact]
        public async Task GetMyTickets_ReturnTicketsResponse()
        {
            List<TicketResponse> response = new()
            {
                new()
                {
                    Id = 1,
                    IdPerformance = 1
                }
            };
            int userId = 1;

            _ticketServiceMock.Setup(ds => ds.GetAllTickets(userId))
                .ReturnsAsync(response);
            _ticketController = new TicketController(_ticketServiceMock.Object, _ticketOptionService.Object);
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Cookie"] = $"UserId={userId}; UserId={userId}";
            _ticketController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            var returnedResult = await _ticketController.GetMyTickets();

            var Result = Assert.IsType<OkObjectResult>(returnedResult);
            Assert.NotNull(Result.Value);
        }

    }
}
