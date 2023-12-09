using BoxTicketApi.DAL.Repositories.Base;
using BoxTicketApi.DAL.Repositories;
using BoxTicketApi.Test.DAL.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.Test.DAL
{
    public class AuthorRepositoryTests : IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture _fixture;
        private readonly IAuthorRepository _authorRepository;

        public AuthorRepositoryTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _authorRepository = new AuthorRepository(_fixture.Context);
        }

        [Fact]
        public async Task GetAllAsync_ShouldreturnListWithAuthor()
        {
            var author = await _authorRepository.GetAllAsync();

            Assert.NotNull(author);
        }
    }
}
