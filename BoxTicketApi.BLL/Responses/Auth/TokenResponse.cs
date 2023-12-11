using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BoxTicketApi.BLL.Responses.Auth
{
    public class TokenResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        
        [JsonIgnore]
        public int UserId {  get; set; }
        [JsonIgnore]
        public DateTime date { get; set; }
    }
}
