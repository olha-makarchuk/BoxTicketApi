using BoxTicketApi.BLL.Requests.Author;
using BoxTicketApi.BLL.Responses.Author;
using BoxTicketApi.BLL.Responses.Genre;
using BoxTicketApi.BLL.Services.Base;
using BoxTicketApi.DAL.Entities;
using BoxTicketApi.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.BLL.Services
{
    public class AuthorService : IAuthorService
    {
        private AuthorRepository _authorRepository;
        public AuthorService(AuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<AuthorResponse> AddAuthor(AuthorRequest request)
        {
            Author author = new() { FirstName = request.FirstName, LastName = request.LastName, MiddleName = request.MiddleName };
            await _authorRepository.AddAsync(author);
            AuthorResponse response = new() {Id=author.Id, MiddleName = request.MiddleName, LastName = request.LastName, FirstName = request.FirstName };
            return response;
        }

        public async Task<List<AuthorResponse>> GetAllAuthor()
        {
            var authors = await _authorRepository.GetAllAsync();
            List<AuthorResponse> responseList = new();

            foreach (var author in authors)
            {
                AuthorResponse response = new AuthorResponse();
                response.Id = author.Id;
                response.FirstName = author.FirstName;
                response.LastName = author.LastName;
                response.MiddleName = author.MiddleName;

                responseList.Add(response);
            }
            return responseList;
        }
    }
}
