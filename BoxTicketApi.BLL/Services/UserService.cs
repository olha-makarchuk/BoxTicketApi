using AutoMapper;
using Azure.Core;
using BoxTicketApi.BLL.Requests.Auth;
using BoxTicketApi.BLL.Responses.Auth;
using BoxTicketApi.BLL.Services.Base;
using BoxTicketApi.DAL.Contexts;
using BoxTicketApi.DAL.Entities;
using BoxTicketApi.DAL.Repositories;
using BoxTicketApi.DAL.Repositories.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace BoxTicketApi.BLL.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IRefreshTokenRepository _refreshTokenRepository;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly JwtSecurityTokenHandler _jwtSecurityToken;

        public UserService(IUserRepository repository, IConfiguration config, IMapper mapper, IRefreshTokenRepository refreshTokenRepository, JwtSecurityTokenHandler jwtSecurityToken)
        {
            _jwtSecurityToken = jwtSecurityToken;
            _userRepository = repository;
            _config = config;
            _refreshTokenRepository = refreshTokenRepository;
            _mapper = mapper;
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

            return _mapper.Map<AuthResponse>(request);
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

            TokenResponse response = new();
            response.AccessToken = CreateToken(existingUser!, existingUser.IdRoleNavigation.NameRole);
            await GenerateRefreshToken(idUser);

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

            TokenResponse response = new();
            response.AccessToken = CreateToken(existingUser, existingUser.IdRoleNavigation.NameRole);
            response.UserId = existingUser.Id;
            response.date = await GenerateRefreshToken(existingUser.Id);

            return response;
        }

        private async Task<DateTime> GenerateRefreshToken(int idUser)
        {
            var refreshToken = await _refreshTokenRepository.GetRefreshTokenByUser(idUser);

            refreshToken.Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            refreshToken.Expires = DateTime.Now.AddDays(1);

            await _refreshTokenRepository.UpdateAsync(refreshToken);

            return refreshToken.Expires.Value;
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

            var jwt = _jwtSecurityToken.WriteToken(token);

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
