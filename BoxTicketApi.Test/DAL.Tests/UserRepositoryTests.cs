using BoxTicketApi.DAL.Repositories.Base;
using BoxTicketApi.DAL.Repositories;
using BoxTicketApi.Test.DAL.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.Test.DAL.Test
{
    public class UserRepositoryTests : IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture _fixture;
        private readonly IUserRepository _userRepository;

        public UserRepositoryTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _userRepository = new UserRepository(_fixture.Context);
        }

        [Fact]
        public async Task GetUserByEmailAsync_WithValidEmail_ShouldGetUser()
        {
            string email = "email2";

            var user = await _userRepository.GetUserByEmailAsync(email);

            Assert.NotNull(user);
        }

        [Fact]
        public async Task GetUserByEmailAsync_WithInValidEmail_ShouldGetNull()
        {
            string email = "email10";

            var user = await _userRepository.GetUserByEmailAsync(email);

            Assert.Null(user);
        }
    }
}
