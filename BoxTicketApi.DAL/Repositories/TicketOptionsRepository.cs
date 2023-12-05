using BoxTicketApi.DAL.Contexts;
using BoxTicketApi.DAL.Entities;
using BoxTicketApi.DAL.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.DAL.Repositories
{
    public class TicketOptionsRepository : GenericRepository<AllTicket>, ITicketOptionsRepository
    {
        public TicketOptionsRepository(BoxTicketContext context) : base(context)
        {
        }

        public async Task<List<AllTicket>> GetAllTickets(int performanceId)
        {
            var allTickets = await _context.AllTickets
                .Where(t => t.IdPerformance == performanceId)
                .Include(t => t.IdTypeNavigation)
                .Include(t => t.IdPerformanceNavigation)
                .ToListAsync();
            return allTickets;
        }
        public async Task<List<int>> GetBoughtSeatsByType(int performanceId, int idOption)
        {
            var purchasedSeats = await _context.Tickets
                .Where(t => t.IdAllTicketsNavigation.IdPerformance == performanceId && t.IdAllTicketsNavigation.IdType == idOption)
                .Select(t => t.SeatNumber)
                .ToListAsync();

            return purchasedSeats;
        }
    }
}
