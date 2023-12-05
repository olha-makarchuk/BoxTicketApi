using BoxTicketApi.DAL.Contexts;
using BoxTicketApi.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.DAL.Repositories
{
    public class AuthorRepository : GenericRepository<Author>
    {
        public AuthorRepository(BoxTicketContext context) : base(context)
        {
        }
    }
}
