using BoxTicketApi.DAL.Contexts;
using BoxTicketApi.DAL.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.DAL.Repositories
{
    public class UserRepository : GenericRepository<UserAccount>, IUserRepository
    {
        public UserRepository(BoxTicketContext context) : base(context)
        {
        }

        public async Task<UserAccount> GetUserByEmailAsync(string email)
        {
            var user =  await _context.UserAccounts
                .Where(u => u.Email == email)
                .Include(u => u.IdRoleNavigation)
                .FirstOrDefaultAsync();
            return user;
        }
    }
}
