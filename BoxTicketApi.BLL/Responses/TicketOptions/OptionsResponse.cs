using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.BLL.Responses.TicketOptions
{
    public class OptionsResponse
    {
        public int Id {  get; set; }
        public string NamePerformance { get; set; }
        public string TypeName {  get; set; }
        public int Price { get; set; }
        public int CoutOfTickets { get; set; }

        public List<int> Seats { get; set; }=new();
    }
}
