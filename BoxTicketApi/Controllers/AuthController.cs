using Azure.Core;
using BoxTicketApi.BLL.Requests.Auth;
using BoxTicketApi.BLL.Responses.Auth;
using BoxTicketApi.BLL.Services.Base;
using BoxTicketApi.DAL.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public static UserAccount user = new UserAccount();
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AuthController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
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

            return Ok(result);
        }
    }
}
