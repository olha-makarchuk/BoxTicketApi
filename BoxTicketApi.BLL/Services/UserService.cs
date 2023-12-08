using AutoMapper;
using Azure;
using Azure.Core;
using BoxTicketApi.BLL.Requests.Auth;
using BoxTicketApi.BLL.Responses.Auth;
using BoxTicketApi.BLL.Services.Base;
using BoxTicketApi.DAL.Contexts;
using BoxTicketApi.DAL.Entities;
using BoxTicketApi.DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.BLL.Services
{
    public class UserService : IUserService
    {
        private UserRepository _userRepository;
        private RefreshTokenRepository _refreshTokenRepository;
        private IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;

        //private readonly IMapper _mapper;
        public UserService(UserRepository repository, IConfiguration config/*, IMapper mapper*/, IHttpContextAccessor httpContextAccessor, RefreshTokenRepository refreshTokenRepository)
        {
            _userRepository = repository;
            _config = config;
            _httpContextAccessor = httpContextAccessor;
            _refreshTokenRepository = refreshTokenRepository;
            //_mapper = mapper;
        }


        public async Task<AuthResponse> RegisterUserAsync(SignUpRequest request)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(request.Email);

            if (existingUser != null)
            {
                throw new Exception("User already exists.");
            }

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            UserAccount user = new();
            user.FirstName = request.FirstName;
            user.MiddleName = request.MiddleName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.IdRole = 2;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            await _userRepository.AddAsync(user);

            await _refreshTokenRepository.AddAsync(new RefreshToken { IdUser = user.Id});

            AuthResponse response = new();
            response.Email = request.Email;
            response.Password = request.Password;

            return response;
        }

        public async Task<TokenResponse> RefreshToken(string refreshToken, int idUser)
        {
            var tokenUser = await _refreshTokenRepository.GetRefreshTokenByUser(idUser);
            var existingUser = await _userRepository.GetByIdAsync(idUser);

            if (tokenUser.Token != refreshToken)
            {
                throw new Exception("Invalid Refresh Token.");
            }
            else if (tokenUser.Expires < DateTime.Now)
            {
                throw new Exception("Token expired.");
            }

            string token = CreateToken(existingUser!, existingUser.IdRoleNavigation.NameRole);
            await GenerateRefreshToken(idUser);

            TokenResponse response = new();
            response.AccessToken = token;

            return response;
        }

        public async Task<TokenResponse> Login(SignInRequest request)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(request.Email);

            if (existingUser == null)
            {
                throw new Exception($"User doesn't exist with this email: {request.Email}.");
            }

            if (!VerifyPasswordHash(request.Password, existingUser.PasswordHash, existingUser.PasswordSalt))
            {
                throw new Exception("Wrong password.");
            }

            string token = CreateToken(existingUser, existingUser.IdRoleNavigation.NameRole);
            SetCookie("UserId", existingUser.Id.ToString());

            await GenerateRefreshToken(existingUser.Id);

            TokenResponse response = new();
            response.AccessToken = token;

            return response;
        }

        private async Task GenerateRefreshToken(int idUser)
        {
            var refreshToken = await _refreshTokenRepository.GetRefreshTokenByUser(idUser);

            refreshToken.Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            refreshToken.Expires = DateTime.Now.AddDays(1);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.Expires
            };
            _httpContextAccessor.HttpContext.Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);

            await _refreshTokenRepository.UpdateAsync(refreshToken);
        }

        private string CreateToken(UserAccount user, string role)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, role),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void SetCookie(string key, string value)
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(1),
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Secure = true
            };

            _httpContextAccessor.HttpContext.Response.Cookies.Append(key, value, cookieOptions);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
