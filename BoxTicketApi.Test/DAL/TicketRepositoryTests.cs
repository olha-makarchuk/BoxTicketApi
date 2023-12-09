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
    public class TicketRepositoryTests : IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture _fixture;
        private readonly ITicketRepository _ticketRepository;

        public TicketRepositoryTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _ticketRepository = new TicketsRepository(_fixture.Context);
        }

        [Fact]
        public async Task GetBoughtSeatsByType_WithIdPerformanceAndIdOption_ShouldReturnCountOfSeats()
        {
            int performanceId = 1;
            int idOption = 2;

            var performances = await _ticketRepository.GetBoughtSeatsByType(performanceId, idOption);

            Assert.NotNull(performances);
            Assert.Single(performances);
        }


        [Fact]
        public async Task GetAllTicketsById_WithIdUser_ShouldReturnListWithAllTypesOfTickets()
        {
            int idUser = 1;

            var performances = await _ticketRepository.GetAllTicketsById(idUser);

            Assert.NotNull(performances);
            Assert.Equal(4, performances.Count);
        }
    }
}
