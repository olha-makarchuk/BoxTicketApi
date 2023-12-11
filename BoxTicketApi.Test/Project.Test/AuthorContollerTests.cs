using BoxTicketApi.BLL.Requests.Auth;
using BoxTicketApi.BLL.Requests.Author;
using BoxTicketApi.BLL.Responses.Auth;
using BoxTicketApi.BLL.Responses.Author;
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
    public class AuthorContollerTests
    {
        private readonly Mock<IAuthorService> _authorServiceMock = new();
        private AuthorController _authorController = null!;

        [Fact]
        public async Task AddAuthor_ReturnAuthorResponse()
        {
            var request = new AuthorRequest()
            {
                FirstName = "first",
                LastName = "last",
                MiddleName = "middle"
            };
            AuthorResponse response = new()
            {
                FirstName = "first",
                LastName = "last",
                MiddleName = "middle",
                Id = 1
            };

            _authorServiceMock.Setup(ds => ds.AddAuthor(request))
                .ReturnsAsync(response);
            _authorController = new AuthorController(_authorServiceMock.Object);

            var returnedResult = await _authorController.AddAuthor(request);

            var Result = Assert.IsType<OkObjectResult>(returnedResult);
            Assert.NotNull(Result.Value);
        }

        [Fact]
        public async Task GetAllAuthor_ReturnAuthorResponse()
        {
            List<AuthorResponse> response = new()
            {
                new()
                {
                    FirstName="first",
                    LastName="last",
                    MiddleName="middle",
                    Id =1
                }
            };

            _authorServiceMock.Setup(ds => ds.GetAllAuthor())
                .ReturnsAsync(response);
            _authorController = new AuthorController(_authorServiceMock.Object);

            var returnedResult = await _authorController.GetAllAuthor();

            var Result = Assert.IsType<OkObjectResult>(returnedResult);
            Assert.NotNull(Result.Value);
        }
    }
}
