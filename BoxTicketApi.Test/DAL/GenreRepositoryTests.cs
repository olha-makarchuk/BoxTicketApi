using BoxTicketApi.DAL.Repositories.Base;
using BoxTicketApi.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoxTicketApi.Test.DAL.Test;
using Xunit;
using BoxTicketApi.DAL.Entities;

namespace BoxTicketApi.Test.DAL
{
    public class GenreRepositoryTests: IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture _fixture;
        private readonly IGenreRepository _genreRepository;

        public GenreRepositoryTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _genreRepository = new GenreRepository(_fixture.Context);
        }

        [Fact]
        public async Task DeleteGenre_WithValidGenreId_ShouldDeleteGenreAndChangeCount()
        {
            var genres = await _genreRepository.GetAllAsync();
            await _genreRepository.DeleteAsync(1);
            var genresAfter = await _genreRepository.GetAllAsync();
            var ifExist = await _genreRepository.GetByIdAsync(1);

            Assert.Null(ifExist);
            Assert.NotEqual(genres.Count, genresAfter.Count);
        }

        [Fact]
        public async Task AddGenre_WithValidGenre_ShouldAddGenreAndChangeCount()
        {
            Genre genreobject = new Genre { NameGenre = "Драма" };

            var genres = await _genreRepository.GetAllAsync();
            await _genreRepository.AddAsync(genreobject);
            var genresAfter = await _genreRepository.GetAllAsync();
            var ifExist = await _genreRepository.GetByIdAsync(genreobject.Id);

            Assert.NotNull(ifExist);
            Assert.NotEqual(genres.Count, genresAfter.Count);
        }
    }
}
