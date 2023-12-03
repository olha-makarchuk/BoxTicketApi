using BoxTicketApi.DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.DAL.Repositories.Base
{
    public interface IUserRepository : IGenericRepository<UserAccount>
    {
        Task<UserAccount> GetUserByEmailAsync(string email);
    }
}
