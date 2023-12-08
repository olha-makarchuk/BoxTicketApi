using BoxTicketApi.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.DAL.Repositories.Base
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetRefreshTokenByUser(int idUser);
    }
}
