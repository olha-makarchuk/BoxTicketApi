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
    public class PerformanceRepository : GenericRepository<Performance>, IPerformanceRepository
    {
        public PerformanceRepository(BoxTicketContext context) : base(context)
        {
        }

        public Task<List<Performance>> GetPerformancesByAuthor(int idAuthor)
        {
            var performances = _context.Performances
                .Where(p => p.IdAuthor == idAuthor)
                .Include(p => p.IdGenreNavigation)
                .Include(p => p.IdAuthorNavigation)
                .ToListAsync();
            return performances;
        }

        public Task<List<Performance>> GetPerformancesByDate(DateTime date)
        {
            var performances = _context.Performances
                .Where(p => p.DateTimeEvent.Date == date.Date)
                .Include(p => p.IdGenreNavigation)
                .Include(p => p.IdAuthorNavigation)
                .ToListAsync();
            return performances;
        }

        public Task<List<Performance>> GetPerformancesByGenre(int idGenre)
        {
            var performances = _context.Performances
                .Where(p => p.IdGenre == idGenre)
                .Include(p => p.IdGenreNavigation)
                .Include(p => p.IdAuthorNavigation)
                .ToListAsync();
            return performances;
        }

        public async Task<List<Performance>> GetPerformancesByName(string name)
        {
            var performances = await _context.Performances
                .Where(p => p.PerformanceName == name)
                .Include(p => p.IdGenreNavigation)
                .Include(p => p.IdAuthorNavigation)
                .ToListAsync();
            return performances;
        }
    }
}
