using AutoMapper;
using BoxTicketApi.BLL.Requests.Author;
using BoxTicketApi.BLL.Responses.Author;
using BoxTicketApi.BLL.Responses.Genre;
using BoxTicketApi.BLL.Services.Base;
using BoxTicketApi.DAL.Entities;
using BoxTicketApi.DAL.Repositories;
using BoxTicketApi.DAL.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.BLL.Services
{
    public class AuthorService : IAuthorService
    {
        private IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        public AuthorService(IAuthorRepository authorRepository, IMapper mapper)
        {
            _mapper = mapper;
            _authorRepository = authorRepository;
        }

        public async Task<AuthorResponse> AddAuthor(AuthorRequest request)
        {
            Author author = new() { FirstName = request.FirstName, LastName = request.LastName, MiddleName = request.MiddleName };
            await _authorRepository.AddAsync(author);
            var a = _mapper.Map<AuthorResponse>(author);
            return a;
        }

        public async Task<List<AuthorResponse>> GetAllAuthor()
        {
            var authors = await _authorRepository.GetAllAsync();

            return _mapper.Map<List<AuthorResponse>>(authors);
        }
    }
}
