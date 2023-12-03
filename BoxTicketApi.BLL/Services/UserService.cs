using AutoMapper;
using BoxTicketApi.BLL.Requests.Auth;
using BoxTicketApi.BLL.Responses.Auth;
using BoxTicketApi.BLL.Services.Base;
using BoxTicketApi.DAL.Contexts;
using BoxTicketApi.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.BLL.Services
{
    public class UserService : IUserService
    {
        private UserAccount user = new(); 
        private readonly UserRepository _repository;
        //private readonly IMapper _mapper;
        public UserService(UserRepository repository/*, IMapper mapper*/)
        {
            _repository = repository;
            //_mapper = mapper;
        }

        Task<AuthResponse> IUserService.Login(SignInRequest user)
        {
            throw new NotImplementedException();
        }

        Task<AuthResponse> IUserService.RegisterAdmin(SignUpRequest user)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthResponse> RegisterUserAsync(SignUpRequest request)
        {
            var existingUser = await _repository.GetUserByEmailAsync(request.Email);

            if (existingUser == null)
            {
                throw new Exception("User already exists.");
            }

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            await _repository.AddAsync(user);

            AuthResponse response = new();

            return response;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

    }
}
