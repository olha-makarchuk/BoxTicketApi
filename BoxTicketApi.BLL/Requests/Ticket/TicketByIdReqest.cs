using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BoxTicketApi.BLL.Requests.Ticket
{
    public class TicketByIdReqest
    {
        [JsonIgnore]
        public int IdUser { get; set; }
        public int Id { get; set; }
    }
}
