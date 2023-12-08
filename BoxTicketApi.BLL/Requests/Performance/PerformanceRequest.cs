using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.BLL.Requests.Performance
{
    public class PerformanceRequest
    {
        public string PerformanceName { get; set; } = null!;

        public int IdGenre { get; set; }

        public int IdAuthor { get; set; }

        public DateTime DateTimeEvent { get; set; }
    }
}
