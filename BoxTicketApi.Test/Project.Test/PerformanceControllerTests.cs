using BoxTicketApi.BLL.Requests.Author;
using BoxTicketApi.BLL.Requests.Performance;
using BoxTicketApi.BLL.Responses.Author;
using BoxTicketApi.BLL.Responses.Performance;
using BoxTicketApi.BLL.Services.Base;
using BoxTicketApi.Controllers;
using BoxTicketApi.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.Test.Project.Test
{
    public class PerformanceControllerTests
    {
        private readonly Mock<IPerformanceService> _performanceServiceMock = new();
        private PerformanceController _performanceController = null!;

        [Fact]
        public async Task AddPerformance_ReturnPerformanceResponse()
        {
            PerformanceRequest request = new()
            {
                IdAuthor=1,
                IdGenre=1,
                DateTimeEvent=DateTime.Now,
                PerformanceName="Performance"
            };

            var response = new PerformanceResponse()
            {
                IdPerformance=1,
                IdAuthor = 1,
                IdGenre = 1,
                DateTimeEvent = DateTime.Now,
                PerformanceName = "Performance"
            };


            _performanceServiceMock.Setup(ds => ds.AddPerformance(request))
                .ReturnsAsync(response);
            _performanceController = new PerformanceController(_performanceServiceMock.Object);

            var returnedResult = await _performanceController.AddPerformances(request);

            var Result = Assert.IsType<OkObjectResult>(returnedResult);
            Assert.NotNull(Result.Value);
        }

        [Fact]
        public async Task GetAllPerformances_ReturnPerformancesResponse()
        {

            var response = new List<PerformanceResponse>()
            {
                new()
                {
                    IdPerformance = 1,
                    IdAuthor = 1,
                    IdGenre = 1,
                    DateTimeEvent = DateTime.Now,
                    PerformanceName = "Performance"
                }
            };

            _performanceServiceMock.Setup(ds => ds.GetAllPerformances())
                .ReturnsAsync(response);
            _performanceController = new PerformanceController(_performanceServiceMock.Object);

            var returnedResult = await _performanceController.GetAllPerformances();

            var Result = Assert.IsType<OkObjectResult>(returnedResult);
            Assert.NotNull(Result.Value);
        }

        [Fact]
        public async Task GetPerformancesByDate_ReturnPerformanceResponse()
        {
            string date = "2023-03-04";
            PerformancesByDateRequest request = new()
            {
                Date = date,
                dateTime = new DateTime(2023, 03, 04)
            };

            var response = new List<PerformanceResponse>()
            {
                new()
                {
                    IdPerformance = 1,
                    IdAuthor = 1,
                    IdGenre = 1,
                    DateTimeEvent = request.dateTime,
                    PerformanceName = "Performance"
                }
            };

            _performanceServiceMock.Setup(ds => ds.GetPerformancesByDate(It.IsAny<PerformancesByDateRequest>()))
                .ReturnsAsync(response);
            _performanceController = new PerformanceController(_performanceServiceMock.Object);

            var returnedResult = await _performanceController.GetPerformancesByDate(date);

            var Result = Assert.IsType<OkObjectResult>(returnedResult);
            Assert.NotNull(Result.Value);
        }

        [Fact]
        public async Task GetPerformancesByAuthor_ReturnPerformanceResponse()
        {
            PerformancesByAuthorRequest request = new()
            {
                idAuthor =1
            };

            var response = new List<PerformanceResponse>()
            {
                new()
                {
                    IdPerformance = 1,
                    IdAuthor = 1,
                    IdGenre = 1,
                    DateTimeEvent = DateTime.Now,
                    PerformanceName = "Performance"
                }
            };

            _performanceServiceMock.Setup(ds => ds.GetPerformancesByAuthor(request))
                .ReturnsAsync(response);
            _performanceController = new PerformanceController(_performanceServiceMock.Object);

            var returnedResult = await _performanceController.GetPerformancesByAuthor(request);

            var Result = Assert.IsType<OkObjectResult>(returnedResult);
            Assert.NotNull(Result.Value);
        }

        [Fact]
        public async Task GetPerformancesByGenre_ReturnPerformanceResponse()
        {
            PerformancesByGenreRequest request = new()
            {
                idGenre = 1
            };

            var response = new List<PerformanceResponse>()
            {
                new()
                {
                    IdPerformance = 1,
                    IdAuthor = 1,
                    IdGenre = 1,
                    DateTimeEvent = DateTime.Now,
                    PerformanceName = "Performance"
                }
            };

            _performanceServiceMock.Setup(ds => ds.GetPerformancesByGenre(request))
                .ReturnsAsync(response);
            _performanceController = new PerformanceController(_performanceServiceMock.Object);

            var returnedResult = await _performanceController.GetPerformancesByGenre(request);

            var Result = Assert.IsType<OkObjectResult>(returnedResult);
            Assert.NotNull(Result.Value);
        }

        [Fact]
        public async Task GetPerformancesByName_ReturnPerformanceResponse()
        {
            PerformancesByNameRequest request = new()
            {
                Name = "Performance"
            };

            var response = new List<PerformanceResponse>()
            {
                new()
                {
                    IdPerformance = 1,
                    IdAuthor = 1,
                    IdGenre = 1,
                    DateTimeEvent = DateTime.Now,
                    PerformanceName = "Performance"
                }
            };

            _performanceServiceMock.Setup(ds => ds.GetPerformancesByName(request))
                .ReturnsAsync(response);
            _performanceController = new PerformanceController(_performanceServiceMock.Object);

            var returnedResult = await _performanceController.GetPerformancesByName(request);

            var Result = Assert.IsType<OkObjectResult>(returnedResult);
            Assert.NotNull(Result.Value);
        }
    }
}
