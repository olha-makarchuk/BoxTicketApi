using AutoMapper;
using BoxTicketApi.BLL.Requests.Genre;
using BoxTicketApi.BLL.Requests.Performance;
using BoxTicketApi.BLL.Responses.Genre;
using BoxTicketApi.BLL.Responses.Performance;
using BoxTicketApi.BLL.Services;
using BoxTicketApi.DAL.Entities;
using BoxTicketApi.DAL.Repositories.Base;
using Moq;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.Test.BLL.Tests
{
    public class PerformanceServiceTests
    {
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<IGenreRepository> _genreRepositoryMock = new();
        private readonly Mock<IPerformanceRepository> _performanceRepositoryMock = new();
        private readonly Mock<IAuthorRepository> _authorRepositoryMock = new();
        private PerformanceService _performanceService = null!;

        public Genre genre = new Genre() { Id = 1, NameGenre = "Genre1" };
        public Author author = new Author() { Id = 1, FirstName = "Test", MiddleName = "Test", LastName = "Test" };

        public Performance performance = new Performance() { IdAuthor = 1, IdGenre = 1, DateTimeEvent = DateTime.Now, PerformanceName = "Performance1" };

        public PerformanceRequest request = new PerformanceRequest() { IdAuthor = 1, IdGenre = 1, DateTimeEvent = DateTime.Now, PerformanceName = "Performance1" };

        public PerformanceResponse expectedPerformance = new PerformanceResponse() { IdAuthor = 1, IdGenre = 1, DateTimeEvent = DateTime.Now, PerformanceName = "Performance1", IdPerformance = 1 };
        
        public List<PerformanceResponse> expectedPerformances = new List<PerformanceResponse>();    
        [Fact]
        public async Task AddPerformance_ShouldReturnPerformance()
        {
            _mapperMock.Setup(m => m.Map<Performance>(request))
                .Returns(performance);
            _mapperMock.Setup(m => m.Map<PerformanceResponse>(It.IsAny<Performance>()))
                .Returns(expectedPerformance);

            _performanceService = new PerformanceService(_mapperMock.Object, _performanceRepositoryMock.Object,  _authorRepositoryMock.Object, _genreRepositoryMock.Object);
            _authorRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(author);
            _genreRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(genre);

            var adderPerformance = await _performanceService.AddPerformance(request);

            Assert.NotNull(adderPerformance);
            Assert.Equal(request.PerformanceName, adderPerformance.PerformanceName);
        }

        [Fact]
        public async Task AddPerformance_ShouldReturnExeptionNotFoundGenre()
        {
            _mapperMock.Setup(m => m.Map<Performance>(request))
                .Returns(performance);
            _mapperMock.Setup(m => m.Map<PerformanceResponse>(It.IsAny<Performance>()))
                .Returns(expectedPerformance);

            _performanceService = new PerformanceService(_mapperMock.Object, _performanceRepositoryMock.Object, _authorRepositoryMock.Object, _genreRepositoryMock.Object);
            _authorRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(author);

            var exception = await Assert.ThrowsAsync<Exception>(async () => await _performanceService.AddPerformance(request));

            Assert.Equal($"Genre with id {request.IdAuthor} not found", exception.Message);
        }

        [Fact]
        public async Task AddPerformance_ShouldReturnExeptionNotFoundAuthor()
        {
            _mapperMock.Setup(m => m.Map<Performance>(request))
                .Returns(performance);
            _mapperMock.Setup(m => m.Map<PerformanceResponse>(It.IsAny<Performance>()))
                .Returns(expectedPerformance);

            _performanceService = new PerformanceService(_mapperMock.Object, _performanceRepositoryMock.Object, _authorRepositoryMock.Object, _genreRepositoryMock.Object);

            _genreRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(genre);

            var exception = await Assert.ThrowsAsync<Exception>(async () => await _performanceService.AddPerformance(request));

            Assert.Equal($"Author with id {request.IdAuthor} not found", exception.Message);
        }

        [Fact]
        public async Task GetAllPerformances_ShouldReturnAllperformances()
        {
            expectedPerformances.Add(expectedPerformance);
            _mapperMock.Setup(m => m.Map<List<PerformanceResponse>>(It.IsAny<Performance>()))
                .Returns(expectedPerformances);
            _performanceService = new PerformanceService(_mapperMock.Object, _performanceRepositoryMock.Object, _authorRepositoryMock.Object, _genreRepositoryMock.Object);

            var performances = await _performanceService.GetAllPerformances();
            
            Assert.NotNull(performances);
            Assert.Single(performances);
        }

        [Fact]
        public async Task GetPerformancesByAuthor_ShouldReturnPerformances()
        {
            List<Performance> performancesList = new List<Performance>();
            performancesList.Add(performance);
            var requestByAuthor = new PerformancesByAuthorRequest() { idAuthor = 1 };
            expectedPerformances.Add(expectedPerformance);
            _mapperMock.Setup(m => m.Map<List<PerformanceResponse>>(It.IsAny<List<Performance>>()))
                .Returns(expectedPerformances);
            _performanceService = new PerformanceService(_mapperMock.Object, _performanceRepositoryMock.Object, _authorRepositoryMock.Object, _genreRepositoryMock.Object);

            _performanceRepositoryMock.Setup(repo => repo.GetPerformancesByAuthor(requestByAuthor.idAuthor))
                .ReturnsAsync(performancesList);

            var performances = await _performanceService.GetPerformancesByAuthor(requestByAuthor);

            Assert.NotNull(performances);
            Assert.Single(performances);
        }

        [Fact]
        public async Task GetPerformancesByAuthor_ShouldReturnExeption()
        {
            List<Performance> performancesList = new List<Performance>();
            var requestByAuthor = new PerformancesByAuthorRequest() { idAuthor = 1 };
            expectedPerformances.Add(expectedPerformance);
            _mapperMock.Setup(m => m.Map<List<PerformanceResponse>>(It.IsAny<List<Performance>>()))
                .Returns(expectedPerformances);
            _performanceService = new PerformanceService(_mapperMock.Object, _performanceRepositoryMock.Object, _authorRepositoryMock.Object, _genreRepositoryMock.Object);
            _performanceRepositoryMock.Setup(repo => repo.GetPerformancesByAuthor(requestByAuthor.idAuthor))
                .ReturnsAsync(performancesList);

            var exception = await Assert.ThrowsAsync<Exception>(async () => await _performanceService.GetPerformancesByAuthor(requestByAuthor));

            Assert.Equal($"Performances with author id {requestByAuthor.idAuthor} not found", exception.Message);
        }

        [Fact]
        public async Task GetPerformancesByDate_WhenValidDate_ShouldReturnPerformances()
        {
            List<Performance> performancesList = new List<Performance>();
            performancesList.Add(performance);
            var requestByDate = new PerformancesByDateRequest();
            requestByDate.Date = "2023-02-02";
            expectedPerformances.Add(expectedPerformance);
            _mapperMock.Setup(m => m.Map<List<PerformanceResponse>>(It.IsAny<List<Performance>>()))
                .Returns(expectedPerformances);
            _performanceService = new PerformanceService(_mapperMock.Object, _performanceRepositoryMock.Object, _authorRepositoryMock.Object, _genreRepositoryMock.Object);

            _performanceRepositoryMock.Setup(repo => repo.GetPerformancesByDate(requestByDate.dateTime))
                .ReturnsAsync(performancesList);

            var performances = await _performanceService.GetPerformancesByDate(requestByDate);

            Assert.NotNull(performances);
            Assert.Single(performances);
        }

        [Fact]
        public void Date_Setter_ShouldSetDateTime()
        {
            // Arrange
            var dateTime = new DateTime(2023, 12, 10);
            var requestByDate = new PerformancesByDateRequest();


            // Assert
            var exception = Assert.Throws<Exception>(() => requestByDate.Date = "2023-13-13");
            Assert.Equal("Неправильний формат дати. Використовуйте формат 'рік-день-місяць'.", exception.Message);
        }

        [Fact]
        public async Task GetPerformancesByDate_ShouldReturnExeption()
        {
            List<Performance> performancesList = new List<Performance>();
            var requestByDate = new PerformancesByDateRequest() { dateTime = new DateTime(2023, 12, 1) };
            expectedPerformances.Add(expectedPerformance);
            _mapperMock.Setup(m => m.Map<List<PerformanceResponse>>(It.IsAny<List<Performance>>()))
                .Returns(expectedPerformances);
            _performanceService = new PerformanceService(_mapperMock.Object, _performanceRepositoryMock.Object, _authorRepositoryMock.Object, _genreRepositoryMock.Object);

            _performanceRepositoryMock.Setup(repo => repo.GetPerformancesByDate(requestByDate.dateTime))
                .ReturnsAsync(performancesList);

            var exception = await Assert.ThrowsAsync<Exception>(async () => await _performanceService.GetPerformancesByDate(requestByDate));

            Assert.Equal($"Performances with date {requestByDate.dateTime} not found", exception.Message);
        }

        [Fact]
        public async Task GetPerformancesByGenre_ShouldReturnPerformances()
        {
            List<Performance> performancesList = new List<Performance>();
            performancesList.Add(performance);
            var requestByGenre = new PerformancesByGenreRequest() { idGenre=1};
            expectedPerformances.Add(expectedPerformance);
            _mapperMock.Setup(m => m.Map<List<PerformanceResponse>>(It.IsAny<List<Performance>>()))
                .Returns(expectedPerformances);
            _performanceService = new PerformanceService(_mapperMock.Object, _performanceRepositoryMock.Object, _authorRepositoryMock.Object, _genreRepositoryMock.Object);

            _performanceRepositoryMock.Setup(repo => repo.GetPerformancesByGenre(requestByGenre.idGenre))
                .ReturnsAsync(performancesList);

            var performances = await _performanceService.GetPerformancesByGenre(requestByGenre);

            Assert.NotNull(performances);
            Assert.Single(performances);
        }

        [Fact]
        public async Task GetPerformancesByGenre_ShouldReturnExeption()
        {
            List<Performance> performancesList = new List<Performance>();
            var requestByGenre = new PerformancesByGenreRequest() { idGenre = 1 };
            expectedPerformances.Add(expectedPerformance);
            _mapperMock.Setup(m => m.Map<List<PerformanceResponse>>(It.IsAny<List<Performance>>()))
                .Returns(expectedPerformances);
            _performanceService = new PerformanceService(_mapperMock.Object, _performanceRepositoryMock.Object, _authorRepositoryMock.Object, _genreRepositoryMock.Object);

            _performanceRepositoryMock.Setup(repo => repo.GetPerformancesByGenre(requestByGenre.idGenre))
                .ReturnsAsync(performancesList);

            var exception = await Assert.ThrowsAsync<Exception>(async () => await _performanceService.GetPerformancesByGenre(requestByGenre));

            Assert.Equal($"Performances with genre id {requestByGenre.idGenre} not found", exception.Message);
        }

        [Fact]
        public async Task GetPerformancesByName_ShouldReturnPerformances()
        {
            List<Performance> performancesList = new List<Performance>();
            performancesList.Add(performance);
            var requestByName = new PerformancesByNameRequest() { Name="name" };
            expectedPerformances.Add(expectedPerformance);
            _mapperMock.Setup(m => m.Map<List<PerformanceResponse>>(It.IsAny<List<Performance>>()))
                .Returns(expectedPerformances);
            _performanceService = new PerformanceService(_mapperMock.Object, _performanceRepositoryMock.Object, _authorRepositoryMock.Object, _genreRepositoryMock.Object);

            _performanceRepositoryMock.Setup(repo => repo.GetPerformancesByName(requestByName.Name))
                .ReturnsAsync(performancesList);

            var performances = await _performanceService.GetPerformancesByName(requestByName);

            Assert.NotNull(performances);
            Assert.Single(performances);
        }

        [Fact]
        public async Task GetPerformancesByName_ShouldReturnExeption()
        {
            List<Performance> performancesList = new List<Performance>();
            var requestByName = new PerformancesByNameRequest() { Name = "name" };
            expectedPerformances.Add(expectedPerformance);
            _mapperMock.Setup(m => m.Map<List<PerformanceResponse>>(It.IsAny<List<Performance>>()))
                .Returns(expectedPerformances);
            _performanceService = new PerformanceService(_mapperMock.Object, _performanceRepositoryMock.Object, _authorRepositoryMock.Object, _genreRepositoryMock.Object);

            _performanceRepositoryMock.Setup(repo => repo.GetPerformancesByName(requestByName.Name))
                .ReturnsAsync(performancesList);

            var exception = await Assert.ThrowsAsync<Exception>(async () => await _performanceService.GetPerformancesByName(requestByName));

            Assert.Equal($"Performances with name {requestByName.Name} not found", exception.Message);
        }
    }
}
