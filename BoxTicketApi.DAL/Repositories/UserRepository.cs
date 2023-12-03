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

        public Task<UserAccount> GetUserByEmailAsync(string email)
        {
            return _context.UserAccounts.Where(u => u.Email == email).FirstOrDefaultAsync()!;
        }
    }
}
