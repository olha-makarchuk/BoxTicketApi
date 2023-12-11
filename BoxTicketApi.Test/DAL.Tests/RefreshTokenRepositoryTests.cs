using BoxTicketApi.DAL.Repositories.Base;
using BoxTicketApi.DAL.Repositories;
using BoxTicketApi.Test.DAL.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoxTicketApi.DAL.Entities;

namespace BoxTicketApi.Test.DAL.Test
{
    public class RefreshTokenRepositoryTests : IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture _fixture;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public RefreshTokenRepositoryTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _refreshTokenRepository = new RefreshTokenRepository(_fixture.Context);
        }

        [Fact]
        public async Task GetRefreshTokenByUser_WithUserId_ShouldGetRefreshToken()
        {
            int userId = 1;
            var token = await _refreshTokenRepository.GetRefreshTokenByUser(userId);

            Assert.NotNull(token);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateRefreshToken()
        {
            string tokenString = "token11";
            RefreshToken token =  new RefreshToken() { Token = tokenString, Expires = DateTime.Now };

            await _refreshTokenRepository.UpdateAsync(token);

            Assert.Equal(token.Token, tokenString);
        }
    }
}
