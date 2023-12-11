using AutoMapper;
using BoxTicketApi.BLL.Requests.Author;
using BoxTicketApi.BLL.Requests.Genre;
using BoxTicketApi.BLL.Responses.Author;
using BoxTicketApi.BLL.Responses.Genre;
using BoxTicketApi.BLL.Services;
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
    public class GenreServiceTests
    {
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<IGenreRepository> _genreRepositoryMock = new();
        private GenreService _genreService = null!;

        [Fact]
        public async Task AddGenreShouldReturnGenre()
        {
            var genre = new Genre
            {
                Id = 1,
                NameGenre = "Genre1"
            };

            var request = new GenreRequest
            {
                NameGenre = "Genre1"
            };

            var expectedGenre = new GenreResponse
            {
                Id = 1,
                NameGenre = "Genre1"
            };

            _mapperMock.Setup(m => m.Map<Genre>(request))
                .Returns(genre);
            _mapperMock.Setup(m => m.Map<GenreResponse>(It.IsAny<Genre>()))
                .Returns(expectedGenre);
            _genreService = new GenreService(_genreRepositoryMock.Object, _mapperMock.Object);

            // Act
            var adderGenre = await _genreService.AddGenre(request);

            // Assert
            Assert.NotNull(adderGenre);
            Assert.Equal(request.NameGenre, adderGenre.NameGenre);
        }

        [Fact]
        public async Task GetAllGenre_ShouldReturnGenres()
        {
            List<Genre> genres = new()
            {
                new Genre(){Id=1, NameGenre="Genre1"},
                new Genre(){Id=2, NameGenre="Genre1"},
                new Genre(){Id=3, NameGenre="Genre1"}
            };

            List<GenreResponse> genresExpected = new()
            {
                new GenreResponse(){Id=1, NameGenre="Genre1"},
                new GenreResponse(){Id=2, NameGenre="Genre1"},
                new GenreResponse(){Id=3, NameGenre="Genre1"}
            };
            
            _mapperMock.Setup(m => m.Map<List<GenreResponse>>(It.IsAny<Author>()))
                .Returns(genresExpected);
            _genreService = new GenreService(_genreRepositoryMock.Object, _mapperMock.Object);

            // Act
            var allAuthors = await _genreService.GetAllGenre();

            // Assert
            Assert.NotNull(allAuthors);
            Assert.Equal(3, allAuthors.Count);
        }
    }
}
