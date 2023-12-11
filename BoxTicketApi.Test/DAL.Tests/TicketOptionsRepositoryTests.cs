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
    public class TicketOptionsRepositoryTests : IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture _fixture;
        private readonly ITicketOptionsRepository _ticketOptionsRepository;

        public TicketOptionsRepositoryTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _ticketOptionsRepository = new TicketOptionsRepository(_fixture.Context);
        }

        [Fact]
        public async Task GetAllTickets_WithIdPerformance_ShouldGetListWithTicketOptions()
        {
            int performanceId = 2;

            var performance = await _ticketOptionsRepository.GetAllTickets(performanceId);

            Assert.NotNull(performance);
            Assert.Equal(2, performance.Count);
        }

        [Fact]
        public async Task GetBoughtSeatsByType_WithIdPerformanceAndIdOption_ShouldReturnCountOfSeats()
        {
            int performanceId = 1;
            int idOption = 2;

            var performances = await _ticketOptionsRepository.GetBoughtSeatsByType(performanceId, idOption);

            Assert.NotNull(performances);
            Assert.Single(performances);
        }
    }
}
