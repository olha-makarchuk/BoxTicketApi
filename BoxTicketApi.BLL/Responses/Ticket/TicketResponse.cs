using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.BLL.Responses.Ticket
{
    public class TicketResponse
    {
        public int Id { get; set; }

        public int IdUser { get; set; }

        public int IdTicketOptions { get; set; }

        public int IdPerformance { get; set; }

        public string Performance { get; set; }

        public string Status {  get; set; }

        public int Price {  get; set; }

        public int SeatNumber { get; set; }
    }
}
