using BoxTicketApi.BLL.Requests.Author;
using BoxTicketApi.BLL.Responses.Author;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.BLL.Services.Base
{
    public interface IAuthorService
    {
        Task<List<AuthorResponse>> GetAllAuthor();
        Task<AuthorResponse> AddAuthor(AuthorRequest request);
    }
}
