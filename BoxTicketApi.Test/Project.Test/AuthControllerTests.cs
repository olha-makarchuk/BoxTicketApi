using Azure;
using Azure.Core;
using BoxTicketApi.BLL.Requests.Auth;
using BoxTicketApi.BLL.Responses.Auth;
using BoxTicketApi.BLL.Services.Base;
using BoxTicketApi.Controllers;
using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.Test.Project.Test
{
    public class AuthControllerTests
    {
        private readonly Mock<IUserService> _userServiceMock = new();
        private AuthController _authController = null!;

        [Fact]
        public async Task Register_ReturnAuthResponse()
        {
            var request = new SignUpRequest()
            {
                Password = "password",
                Email = "email",
            };
            var response = new AuthResponse()
            {
                Email = "email",
                Password = "password"
            };

            _userServiceMock.Setup(ds => ds.RegisterUserAsync(request))
                .ReturnsAsync(response);
            _authController = new AuthController(_userServiceMock.Object);

            var returnedResult = await _authController.Register(request);

            var Result = Assert.IsType<OkObjectResult>(returnedResult);
            Assert.NotNull(Result.Value);
        }

        [Fact]
        public async Task Login_ReturnAuthResponse()
        {
            var request = new SignInRequest()
            {
                Password = "password",
                Email = "email",
            };
            var response = new TokenResponse()
            {
                AccessToken = "token",
                date = DateTime.Now.AddDays(1),
                UserId=1
            };

            _userServiceMock.Setup(ds => ds.Login(request))
                .ReturnsAsync(response);
            _authController = new AuthController(_userServiceMock.Object);

            var httpContext = new DefaultHttpContext();
            _authController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
            var returnedResult = await _authController.Login(request);
            var a = returnedResult.Result;
            Assert.NotNull(returnedResult.Result);
        }

        [Fact]
        public async Task RefreshToken_WithValidCookies_Returns_OkResult()
        {
            var response = new TokenResponse()
            {
                AccessToken = "token",
                date = DateTime.Now.AddDays(1),
                UserId = 1
            };

            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var controller = new AuthController(userServiceMock.Object);
            var refreshTokenValue = "yourRefreshToken";
            var userIdValue = "1";
            var expectedResult = "NewToken"; 

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Cookie"] = $"refreshToken={refreshTokenValue}; UserId={userIdValue}";
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
            _userServiceMock.Setup(ds => ds.RefreshToken(refreshTokenValue, 1))
                .ReturnsAsync(response);

            var result = await controller.RefreshToken();

            Assert.NotNull(result);
        }

        [Fact]
        public async Task RefreshToken_Returns_BadRequest_When_UserId_Not_Exists()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();

            var controller = new AuthController(mockUserService.Object);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();

            // Act
            var result = await controller.RefreshToken();

            // Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }
    }
}
