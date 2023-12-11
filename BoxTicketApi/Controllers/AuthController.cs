using Azure.Core;
using BoxTicketApi.BLL.Requests.Auth;
using BoxTicketApi.BLL.Responses.Auth;
using BoxTicketApi.BLL.Services;
using BoxTicketApi.BLL.Services.Base;
using BoxTicketApi.DAL.Contexts;
using BoxTicketApi.DAL.Entities;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace BoxTicketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(SignUpRequest request)
        {
            var result = await _userService.RegisterUserAsync(request);

            return Ok(result);
        }
        
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(SignInRequest request)
        {
            var result = await _userService.Login(request);
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = result.date,
                Secure = true
            };
            Response.Cookies.Append("UserId", result.UserId.ToString(), cookieOptions);
            Response.Cookies.Append("refreshToken", result.AccessToken, cookieOptions);

            return Ok(result);
        }

        [HttpGet("refresh-token"), Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (Request.Cookies.TryGetValue("UserId", out string userId))
            {
                var result = await _userService.RefreshToken(refreshToken!, Convert.ToInt32(userId));
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
