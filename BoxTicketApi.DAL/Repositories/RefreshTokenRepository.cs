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
    public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(BoxTicketContext context) : base(context)
        {
        }

        public async Task<RefreshToken> GetRefreshTokenByUser(int idUser)
        {
            var tokens =  await _context.RefreshTokens.Where(t => t.IdUser == idUser).FirstOrDefaultAsync();
            return tokens!;
        }
    }
}
