using AutoMapper;
using Azure.Core;
using BoxTicketApi.BLL.Requests.Auth;
using BoxTicketApi.BLL.Responses.Auth;
using BoxTicketApi.BLL.Services.Base;
using BoxTicketApi.DAL.Contexts;
using BoxTicketApi.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
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
        private UserAccount user = new(); 
        private readonly UserRepository _repository;
        private IConfiguration _config;

        //private readonly IMapper _mapper;
        public UserService(UserRepository repository, IConfiguration config/*, IMapper mapper*/)
        {
            _repository = repository;
            _config = config;
            //_mapper = mapper;
        }


        public async Task<AuthResponse> Login(SignInRequest request)
        {
            var existingUser = await _repository.GetUserByEmailAsync(request.Email);

            if (existingUser == null)
            {
                throw new Exception($"User doesn't exist with this email: {request.Email}.");
            }

            if (!VerifyPasswordHash(request.Password, existingUser.PasswordHash, existingUser.PasswordSalt))
            {
                throw new Exception("Wrong password.");
            }

            string token = CreateToken(existingUser, "Standart");

            //var refreshToken = GenerateRefreshToken();
            //SetRefreshToken(refreshToken);

            AuthResponse response = new();
            response.AccessToken = token;
            return response;
        }

        Task<AuthResponse> IUserService.RegisterAdmin(SignUpRequest user)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthResponse> RegisterUserAsync(SignUpRequest request)
        {
            var existingUser = await _repository.GetUserByEmailAsync(request.Email);

            if (existingUser != null)
            {
                throw new Exception("User already exists.");
            }

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.FirstName = request.FirstName;
            user.MiddleName = request.MiddleName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.IdRole = 2;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            await _repository.AddAsync(user);

            AuthResponse response = new();
            return response;
        }

        private string CreateToken(UserAccount user, string role)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
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
