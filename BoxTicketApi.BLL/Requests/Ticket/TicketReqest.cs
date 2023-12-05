using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BoxTicketApi.BLL.Requests.Ticket
{
    public class TicketReqest
    {
        [JsonIgnore]
        public int IdUser { get; set; }

        public int IdTicketOptions { get; set; }

        public int IdPerformance { get; set; }

        public int SeatNumber { get; set; }
    }
}
