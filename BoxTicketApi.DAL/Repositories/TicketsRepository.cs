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
    public class TicketsRepository : GenericRepository<Ticket>, ITicketRepository
    {
        public TicketsRepository(BoxTicketContext context) : base(context)
        {
        }

        public async Task<List<int>> GetBoughtSeatsByType(int performanceId, int idOption)
        {
            var purchasedSeats = await _context.Tickets
                .Where(t => t.IdAllTicketsNavigation.IdPerformance == performanceId && t.IdAllTicketsNavigation.IdType == idOption)
                .Select(t => t.SeatNumber)
                .ToListAsync();

            return purchasedSeats;
        }
        public async Task<List<Ticket>> GetAllTicketsById(int IdUser)
        {
            var tickets = await _context.Tickets
                .Where(t=> t.IdUser == IdUser)
                .Include(t => t.IdAllTicketsNavigation.IdPerformanceNavigation)
                .Include(t => t.IdStatusNavigation)
                .ToListAsync();

            return tickets;
        }
    }
}
