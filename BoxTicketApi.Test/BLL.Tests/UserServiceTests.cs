using AutoMapper;
using Azure.Core;
using BoxTicketApi.BLL.Mapper;
using BoxTicketApi.BLL.Requests.Auth;
using BoxTicketApi.BLL.Requests.Genre;
using BoxTicketApi.BLL.Responses.Auth;
using BoxTicketApi.BLL.Responses.Genre;
using BoxTicketApi.BLL.Services;
using BoxTicketApi.BLL.Services.Base;
using BoxTicketApi.DAL.Contexts;
using BoxTicketApi.DAL.Entities;
using BoxTicketApi.DAL.Repositories.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Moq;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.Test.BLL.Tests
{
    public class UserServiceTests
    {
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<IUserRepository> _userRepositoryMock = new();
        private readonly Mock<IRefreshTokenRepository> _refreshTokenRepositoryMock = new();
        private readonly Mock<IConfiguration> _configMock = new();
        private UserService _userService = null!;
        
        [Fact]
        public async Task RegisterUserAsync_WhenUserDoesNotExist_ReturnsAuthResponse()
        {
            var signUpRequest = new SignUpRequest
            {
                FirstName = "first",
                MiddleName = "middle",
                LastName = "Last",
                Email = "email",
                Password = "Password"
            };
            Random rnd = new Random();
            byte[] hash = new byte[32]; 
            rnd.NextBytes(hash);

            var user = new UserAccount
            {
                Id = 1,
                FirstName = "first",
                MiddleName = "middle",
                LastName = "Last",
                Email = "email",
                PasswordHash = hash,
                PasswordSalt = hash,
                IdRole = 1
            };

            var authResponse = new AuthResponse
            {
                Email="email",
                Password="password"
            };
            var token = new RefreshToken
            {
                IdUser = 1
            };
            var jwtHandlerMock = new Mock<JwtSecurityTokenHandler>();
            _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(signUpRequest.Email))
                .ReturnsAsync((UserAccount)null);
            _userRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<UserAccount>()))
                .Callback<UserAccount>(user => { user.Id = 1; })
                .Returns(Task.CompletedTask);
            _refreshTokenRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<RefreshToken>()))
                .Callback<RefreshToken>(token => { token.Id = 1; })
                .Returns(Task.CompletedTask);
            _mapperMock.Setup(m => m.Map<AuthResponse>(signUpRequest))
                .Returns(authResponse);
            _userService = new UserService(_userRepositoryMock.Object, _configMock.Object, _mapperMock.Object, _refreshTokenRepositoryMock.Object, jwtHandlerMock.Object);

            var result = await _userService.RegisterUserAsync(signUpRequest);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task RegisterUserAsync_WhenUserExists_ThrowsException()
        {
            var signUpRequest = new SignUpRequest
            {
                FirstName = "first",
                MiddleName = "middle",
                LastName = "Last",
                Email = "email",
                Password = "Password"
            };
            Random rnd = new Random();
            byte[] hash = new byte[32];
            rnd.NextBytes(hash);
            var existingUser = new UserAccount
            {
                Id = 1,
                FirstName = "first",
                MiddleName = "middle",
                LastName = "Last",
                Email = "email",
                PasswordHash = hash,
                PasswordSalt = hash,
                IdRole = 1
            };
            var jwtHandlerMock = new Mock<JwtSecurityTokenHandler>();
            _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(signUpRequest.Email))
                .ReturnsAsync(existingUser);
            _userService = new UserService(_userRepositoryMock.Object, _configMock.Object, _mapperMock.Object, _refreshTokenRepositoryMock.Object, jwtHandlerMock.Object);

            var exception = await Assert.ThrowsAsync<Exception>(async () => await _userService.RegisterUserAsync(signUpRequest));
            Assert.Equal("User already exists.", exception.Message);
        }

        [Fact]
        public async Task RefreshToken_WhenValidRefreshToken_ReturnsTokenResponse()
        {
            int userId = 1; 
            string refreshToken = "valid_refresh_token"; 

            var tokenUser = new RefreshToken
            {
                Token = refreshToken,
                Expires = DateTime.Now.AddDays(1)
            };

            var role = new RoleUser
            {
                Id = 1,
                NameRole = "User"
            };
            var existingUser = new UserAccount
            {
                IdRole = userId,
                Id = userId,
                FirstName = "first",
                MiddleName = "middle",
                LastName = "Last",
                Email = "email",
                IdRoleNavigation = role
            };
            var jwtHandlerMock = new Mock<JwtSecurityTokenHandler>();
            jwtHandlerMock.Setup(x => x.WriteToken(It.IsAny<SecurityToken>()))
                .Returns("MockedJWTToken");
            _configMock.Setup(x => x.GetSection("AppSettings:Token").Value).Returns("your_token_value");
            _refreshTokenRepositoryMock.Setup(repo => repo.GetRefreshTokenByUser(userId))
                .ReturnsAsync(tokenUser);
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId))
                .ReturnsAsync(existingUser);
            _userService = new UserService(_userRepositoryMock.Object, _configMock.Object, _mapperMock.Object, _refreshTokenRepositoryMock.Object, jwtHandlerMock.Object);

            var result = await _userService.RefreshToken(refreshToken, userId);

            Assert.NotNull(result);
        }
        
        [Fact]
        public async Task RefreshToken_WhenInvalidRefreshToken_ReturnsExeption()
        {
            int userId = 1;
            string refreshTokenInvalid = "invalid_refresh_token";
            string refreshToken = "valid_refresh_token";

            var tokenUserValid = new RefreshToken
            {
                Token = refreshToken,
                Expires = DateTime.Now.AddDays(1)
            };
            var tokenUserInValid = new RefreshToken
            {
                Token = refreshToken,
                Expires = DateTime.Now.AddDays(1)
            };

            var existingUser = new UserAccount
            {
                Id = userId
            };
            var jwtHandlerMock = new Mock<JwtSecurityTokenHandler>();
            _refreshTokenRepositoryMock.Setup(repo => repo.GetRefreshTokenByUser(userId))
                .ReturnsAsync(tokenUserValid);
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId))
                .ReturnsAsync(existingUser);
            _userService = new UserService(_userRepositoryMock.Object, _configMock.Object, _mapperMock.Object, _refreshTokenRepositoryMock.Object, jwtHandlerMock.Object);

            var exception = await Assert.ThrowsAsync<Exception>(async () => await _userService.RefreshToken(refreshTokenInvalid, userId));

            Assert.Equal($"Invalid Refresh Token.", exception.Message);
        }

        [Fact]
        public async Task RefreshToken_WhenInvalidExpiresDate_ReturnsExeption()
        {
            int userId = 1;
            string refreshToken = "valid_refresh_token";

            var tokenUserValid = new RefreshToken
            {
                Token = refreshToken,
                Expires = new DateTime (2023,1,1)
            };

            var existingUser = new UserAccount
            {
                Id = userId
            };
            var jwtHandlerMock = new Mock<JwtSecurityTokenHandler>();
            _refreshTokenRepositoryMock.Setup(repo => repo.GetRefreshTokenByUser(userId))
                .ReturnsAsync(tokenUserValid);
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId))
                .ReturnsAsync(existingUser);
            _userService = new UserService(_userRepositoryMock.Object, _configMock.Object, _mapperMock.Object, _refreshTokenRepositoryMock.Object, jwtHandlerMock.Object);

            var exception = await Assert.ThrowsAsync<Exception>(async () => await _userService.RefreshToken(refreshToken, userId));

            Assert.Equal($"Token expired.", exception.Message);
        }

        [Fact]
        public async Task Login_WhenValidPassword_ReturnsTokenResponse()
        {
            int idUser = 1;
            string tokenUser = "tokenUser";
            string tokenUpdate = "tokenUser";
            string password = "passwordUser";
            byte[] passwordHash;
            byte[] passwordSalt;

            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            var signInRequest = new SignInRequest
            {
                Email = "string",
                Password = password
            };
            var role = new RoleUser
            {
                Id = 1,
                NameRole = "User"
            };
            var existingUser = new UserAccount
            {
                Id = idUser,
                FirstName = "first",
                MiddleName = "middle",
                LastName = "Last",
                Email = "email",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                IdRoleNavigation= role,
                IdRole = 1
            };
            var token = new RefreshToken
            {
                Token = tokenUser,
                Expires = DateTime.Now.AddDays(1)
            };
            var jwtHandlerMock = new Mock<JwtSecurityTokenHandler>();
            _configMock.Setup(x => x.GetSection("AppSettings:Token").Value).Returns("your_token_value");
            _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(signInRequest.Email))
                .ReturnsAsync(existingUser);
            _refreshTokenRepositoryMock.Setup(repo => repo.GetRefreshTokenByUser(idUser))
                .ReturnsAsync(token);
            _userService = new UserService(_userRepositoryMock.Object, _configMock.Object, _mapperMock.Object, _refreshTokenRepositoryMock.Object, jwtHandlerMock.Object);

            var result = await _userService.Login(signInRequest);

            Assert.NotNull(result);
            Assert.Equal(idUser, result.UserId);
        }

        [Fact]
        public async Task Login_WhenInValidEmail_ReturnExeption()
        {
            int idUser = 1;
            string tokenUser = "tokenUser";
            string tokenUpdate = "tokenUser";
            string password = "passwordUser";
            byte[] passwordHash;
            byte[] passwordSalt;

            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            var signInRequest = new SignInRequest
            {
                Email = "string",
                Password = password
            };
            var role = new RoleUser
            {
                Id = 1,
                NameRole = "User"
            };
            var existingUser = new UserAccount
            {
                Id = idUser,
                FirstName = "first",
                MiddleName = "middle",
                LastName = "Last",
                Email = "email",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                IdRoleNavigation = role,
                IdRole = 1
            };
            var token = new RefreshToken
            {
                Token = tokenUser,
                Expires = DateTime.Now.AddDays(1)
            };
            var jwtHandlerMock = new Mock<JwtSecurityTokenHandler>();
            UserAccount userNull = null;
            _configMock.Setup(x => x.GetSection("AppSettings:Token").Value).Returns("your_token_value");
            _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(signInRequest.Email))
                .ReturnsAsync(userNull);
            _refreshTokenRepositoryMock.Setup(repo => repo.GetRefreshTokenByUser(idUser))
                .ReturnsAsync(token);
            _userService = new UserService(_userRepositoryMock.Object, _configMock.Object, _mapperMock.Object, _refreshTokenRepositoryMock.Object, jwtHandlerMock.Object);

            var exception = await Assert.ThrowsAsync<Exception>(async () => await _userService.Login(signInRequest));

            Assert.Equal($"User doesn't exist with this email: {signInRequest.Email}.", exception.Message);
        }

        [Fact]
        public async Task Login_WhenInValidPassword_ReturnExeption()
        {
            int idUser = 1;
            string tokenUser = "tokenUser";
            string tokenUpdate = "tokenUser";
            string password = "passwordUser";
            string passwordInvalid = "invalidPasswordUser";

            byte[] passwordHash;
            byte[] passwordSalt;

            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            var signInRequest = new SignInRequest
            {
                Id = idUser,
                Email = "email",
                Password = passwordInvalid
            };
            var role = new RoleUser
            {
                Id = 1,
                NameRole = "User"
            };
            var existingUser = new UserAccount
            {
                Id = idUser,
                FirstName = "first",
                MiddleName = "middle",
                LastName = "Last",
                Email = "email",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                IdRoleNavigation = role,
                IdRole = 1
            };
            var token = new RefreshToken
            {
                Token = tokenUser,
                Expires = DateTime.Now.AddDays(1)
            };
            var jwtHandlerMock = new Mock<JwtSecurityTokenHandler>();
            _configMock.Setup(x => x.GetSection("AppSettings:Token").Value).Returns("your_token_value");
            _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(signInRequest.Email))
                .ReturnsAsync(existingUser);
            _refreshTokenRepositoryMock.Setup(repo => repo.GetRefreshTokenByUser(idUser))
                .ReturnsAsync(token);
            _userService = new UserService(_userRepositoryMock.Object, _configMock.Object, _mapperMock.Object, _refreshTokenRepositoryMock.Object, jwtHandlerMock.Object);

            var exception = await Assert.ThrowsAsync<Exception>(async () => await _userService.Login(signInRequest));

            Assert.Equal($"Wrong password.", exception.Message);
        }
    }
}
