using BoxTicketApi.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.BLL.Responses.Performance
{
    public class PerformanceResponse
    {
        public int IdPerformance { get; set; }

        public string PerformanceName { get; set; } = null!;

        public int IdGenre { get; set; }

        public int IdAuthor { get; set; }

        public DateTime DateTimeEvent { get; set; }

    }
}
