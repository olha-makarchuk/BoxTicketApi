using BoxTicketApi.BLL.Requests.Auth;
using BoxTicketApi.BLL.Requests.TicketOptions;
using BoxTicketApi.BLL.Responses.Auth;
using BoxTicketApi.BLL.Responses.TicketOptions;
using BoxTicketApi.BLL.Services.Base;
using BoxTicketApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.Test.Project.Test
{
    public class TicketOptionControllerTests
    {
        private readonly Mock<ITicketOptionsService> _ticketOptionServiceMock = new();
        private TicketOptionsController _ticketOptionController = null!;

        [Fact]
        public async Task AvaillableTickets_ReturnOptionResponse()
        {
            var request = new GetOptionsRequest()
            {
                IdPerformance=1
            };
            var response = new List<OptionsResponse>()
            {
                new()
                {
                    CoutOfTickets=30,
                    NamePerformance="performance",
                    Id=1
                }
            };

            _ticketOptionServiceMock.Setup(ds => ds.GetAllAvailableTickets(request))
                .ReturnsAsync(response);
            _ticketOptionController = new TicketOptionsController(_ticketOptionServiceMock.Object);

            var returnedResult = await _ticketOptionController.GetAvaillableTickets(request);

            var Result = Assert.IsType<OkObjectResult>(returnedResult);
            Assert.NotNull(Result.Value);
        }
    }
}
