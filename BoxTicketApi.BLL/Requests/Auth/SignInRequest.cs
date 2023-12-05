using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BoxTicketApi.BLL.Requests.Auth
{
    public class SignInRequest
    {
        [JsonIgnore]
        public int Id {  get; set; } 
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
