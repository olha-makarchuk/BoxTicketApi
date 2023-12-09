using BoxTicketApi.DAL.Contexts;
using BoxTicketApi.DAL.Entities;
using BoxTicketApi.DAL.Repositories;
using BoxTicketApi.DAL.Repositories.Base;
using BoxTicketApi.Test.DAL.Test;
using Dasync.Collections;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BoxTicketApi.Test.DAL
{
    public class PerformanceRepositoryTests : IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture _fixture;
        private readonly IPerformanceRepository _performanceRepository;

        public PerformanceRepositoryTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _performanceRepository = new PerformanceRepository(_fixture.Context);
        }

        [Fact]
        public async Task GetPerformancesByAuthor_ShouldReturnPerformancesForGivenAuthor()
        {
            var performances = await _performanceRepository.GetPerformancesByAuthor(1);

            Assert.NotNull(performances);
            Assert.Equal(1, performances.Count);
        }

        [Fact]
        public async Task GetPerformancesByAuthor_ShouldReturnPerformancesForGivenDate()
        {
            var performances = await _performanceRepository.GetPerformancesByDate(new DateTime(2023,1,2));

            Assert.NotNull(performances);
            Assert.Equal(1, performances.Count);
        }

        [Fact]
        public async Task GetPerformancesByAuthor_ShouldReturnPerformancesForGivenGenre()
        {
            var performances = await _performanceRepository.GetPerformancesByGenre(2);

            Assert.NotNull(performances);
            Assert.Equal(1, performances.Count);
        }

        [Fact]
        public async Task GetPerformancesByAuthor_ShouldReturnPerformancesForGivenName()
        {
            var performances = await _performanceRepository.GetPerformancesByName("Назва1");

            Assert.NotNull(performances);
            Assert.Equal(1, performances.Count);
        }
    }
}
