using BoxTicketApi.BLL.Requests.Auth;
using BoxTicketApi.BLL.Requests.Genre;
using BoxTicketApi.BLL.Responses.Auth;
using BoxTicketApi.BLL.Responses.Genre;
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
    public class GenreControllerTests
    {
        private readonly Mock<IGenreService> _genreServiceMock = new();
        private GenreController _genreController = null!;

        [Fact]
        public async Task GetAllGenre_ReturnGenreResponse()
        {
            List<GenreResponse> genre = new()
            {
                new()
                {
                    NameGenre="genre"
                }
            };

            _genreServiceMock.Setup(ds => ds.GetAllGenre())
                .ReturnsAsync(genre);
            _genreController = new GenreController(_genreServiceMock.Object);

            var returnedResult = await _genreController.GetAllGenre();

            var Result = Assert.IsType<OkObjectResult>(returnedResult);
            Assert.NotNull(Result.Value);
        }

        [Fact]
        public async Task AddGenre_ReturnGenreResponse()
        {
            GenreRequest request = new()
            {
                NameGenre = "genre"
            };

            GenreResponse genre = new()
            {
                Id = 1,
                NameGenre="genre"
            };

            _genreServiceMock.Setup(ds => ds.AddGenre(request))
                .ReturnsAsync(genre);
            _genreController = new GenreController(_genreServiceMock.Object);

            var returnedResult = await _genreController.AddGenre(request);

            var Result = Assert.IsType<OkObjectResult>(returnedResult);
            Assert.NotNull(Result.Value);
        }
    }
}
