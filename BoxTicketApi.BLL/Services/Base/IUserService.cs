using BoxTicketApi.BLL.Requests.Auth;
using BoxTicketApi.BLL.Responses.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.BLL.Services.Base
{
    public interface IUserService
    {
        Task<AuthResponse> RegisterUserAsync(SignUpRequest user);
        Task<AuthResponse> Login(SignInRequest user);
    }
}
