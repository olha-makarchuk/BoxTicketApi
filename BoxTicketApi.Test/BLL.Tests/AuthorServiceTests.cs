using AutoMapper;
using BoxTicketApi.BLL.Requests.Author;
using BoxTicketApi.BLL.Responses.Author;
using BoxTicketApi.BLL.Services;
using BoxTicketApi.DAL.Entities;
using BoxTicketApi.DAL.Repositories;
using BoxTicketApi.DAL.Repositories.Base;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.Test.BLL.Tests
{
    public class AuthorServiceTests
    {
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<IAuthorRepository> _authorRepositoryMock = new();
        private AuthorService _authorService = null!;

        [Fact]
        public async Task AddAuthor_ShouldReturnAuthor()
        {
            var author = new Author
            {
                Id = 1,
                FirstName = "Name1",
                LastName = "Last1",
                MiddleName = "Middle1",
            };

            var request = new AuthorRequest
            {
                FirstName = "Name1",
                LastName = "Last1",
                MiddleName = "Middle1",
            };

            var expectedAuthor = new AuthorResponse
            {
                Id = 1,
                FirstName = "Name1",
                LastName = "Last1",
                MiddleName = "Middle1",
            };

            _mapperMock.Setup(m => m.Map<Author>(request))
                .Returns(author);
            _mapperMock.Setup(m => m.Map<AuthorResponse>(It.IsAny<Author>()))
                .Returns(expectedAuthor);
            _authorService = new AuthorService(_authorRepositoryMock.Object, _mapperMock.Object);

            // Act
            var adderAuthor = await _authorService.AddAuthor(request);

            // Assert
            Assert.NotNull(adderAuthor);
            Assert.Equal(request.LastName, adderAuthor.LastName);
        }

        [Fact]
        public async Task GetAllAuthor_ShouldReturnAuthors()
        {
            List<Author> authors = new()
            {
                new Author{Id = 1, FirstName = "Name1", LastName = "Last1", MiddleName = "Middle1"},
                new Author{Id = 2, FirstName = "Name2", LastName = "Last2", MiddleName = "Middle2"},
                new Author{Id = 3, FirstName = "Name3", LastName = "Last3", MiddleName = "Middle3"},
            };

            List<AuthorResponse> authorsExpecter = new()
            {
                new AuthorResponse{Id = 1, FirstName = "Name1", LastName = "Last1", MiddleName = "Middle1"},
                new AuthorResponse{Id = 2, FirstName = "Name2", LastName = "Last2", MiddleName = "Middle2"},
                new AuthorResponse{Id = 3, FirstName = "Name3", LastName = "Last3", MiddleName = "Middle3"},
            };
            var author = new Author
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                MiddleName = "Smith",
            };

            _mapperMock.Setup(m => m.Map<List<AuthorResponse>>(It.IsAny<Author>()))
                .Returns(authorsExpecter);
            _authorService = new AuthorService(_authorRepositoryMock.Object, _mapperMock.Object);

            // Act
            var allAuthors = await _authorService.GetAllAuthor();

            // Assert
            Assert.NotNull(allAuthors);
            Assert.Equal(3, allAuthors.Count);
        }
    }
}
