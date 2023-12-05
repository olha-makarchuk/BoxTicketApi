using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.BLL.Responses.Auth
{
    public class RefreshTokenResponse
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Expires { get; set; }
    }
}
