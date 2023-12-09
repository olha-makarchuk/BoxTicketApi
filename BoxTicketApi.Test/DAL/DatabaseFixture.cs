using BoxTicketApi.DAL.Contexts;
using BoxTicketApi.DAL.Entities;
using BoxTicketApi.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dasync.Collections;
using BoxTicketApi.DAL.Repositories.Base;

namespace BoxTicketApi.Test.DAL.Test
{
    public class DatabaseFixture: IDisposable
    {
        public BoxTicketContext Context;

        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<BoxTicketContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            Context = new BoxTicketContext(options);
            SeedTestData().Wait();
        }

        private async Task SeedTestData()
        {
            byte[] pass = new byte[10];
            byte[] salt = new byte[5];

            Random random = new Random();

            List<RoleUser> usersRole = new List<RoleUser>()
            {
                new RoleUser() { NameRole = "Admin"},
                new RoleUser() { NameRole = "User" }
            };
            await Context.RoleUsers.AddRangeAsync(usersRole);

            List<StatusTicket> statusTickets = new List<StatusTicket>
            {
                new StatusTicket() { StatusName = "Куплений" },
                new StatusTicket() { StatusName = "Заброньований"}
            };
            await Context.StatusTickets.AddRangeAsync(statusTickets);

            List<TypeOfTicket> types = new List<TypeOfTicket>()
            {
                new TypeOfTicket() { TypeName = "Звичайний" },
                new TypeOfTicket() { TypeName = "Покращений" },
                new TypeOfTicket() { TypeName = "Люкс" }
            };
            await Context.TypeOfTickets.AddRangeAsync(types);

            List<Author> authors = new List<Author>()
            {
                new Author { FirstName = "Анастасія", MiddleName = "Михайлівна", LastName = "Сидоренко" },
                new Author { FirstName = "Ігор", MiddleName = "Іванович", LastName = "Уманчук" }
            };
            await Context.Authors.AddRangeAsync(authors);

            List<Genre> genres = new List<Genre>()
            {
                new Genre { NameGenre = "Драма" },
                new Genre { NameGenre = "Фікшн" }
            };
            await Context.Genres.AddRangeAsync(genres);

            random.NextBytes(pass);
            random.NextBytes(salt);
            UserAccount userAccount1 = new UserAccount() { FirstName = "first1", MiddleName = "middle1", LastName = "last1", Email = "email1", IdRole = 2, PasswordHash = pass, PasswordSalt= salt};
            random.NextBytes(pass);
            random.NextBytes(salt);
            UserAccount userAccount2 = new UserAccount() { FirstName = "first2", MiddleName = "middle2", LastName = "last2", Email = "email2", IdRole = 1, PasswordHash = pass, PasswordSalt = salt };
            await Context.UserAccounts.AddAsync(userAccount1);
            await Context.UserAccounts.AddAsync(userAccount2);
            await Context.SaveChangesAsync();

            List<RefreshToken> refreshTokens = new List<RefreshToken>()
            {
                new RefreshToken() { Token="token1", Expires = DateTime.Now, IdUser=1},
                new RefreshToken() { Token = "token2", Expires = DateTime.Now, IdUser = 2 }
            };
            await Context.RefreshTokens.AddRangeAsync(refreshTokens);

            List<Performance> performances = new List<Performance>()
            {
                new Performance{PerformanceName = "Назва1", DateTimeEvent = new DateTime (2023, 1, 2), IdAuthor=1, IdGenre=2},
                new Performance { PerformanceName = "Назва2", DateTimeEvent = new DateTime(2023, 5, 8), IdAuthor = 2, IdGenre = 1}
            };
            await Context.Performances.AddRangeAsync(performances);
            await Context.SaveChangesAsync();

            List<AllTicket> options = new List<AllTicket>()
            {
                new AllTicket() { IdType = 1, IdPerformance = 1, CoutOfTickets = 50, Price = 100},
                new AllTicket() { IdType = 2, IdPerformance = 1, CoutOfTickets = 30, Price = 200 },
                new AllTicket() { IdType = 3, IdPerformance = 1, CoutOfTickets = 20, Price = 300 },
                new AllTicket() { IdType = 1, IdPerformance = 2, CoutOfTickets = 60, Price = 100 },
                new AllTicket() { IdType = 2, IdPerformance = 2, CoutOfTickets = 20, Price = 200 }
            };
            await Context.AllTickets.AddRangeAsync(options);
            await Context.SaveChangesAsync();

            List<Ticket> tickets = new List<Ticket>()
            {
                new Ticket() { IdAllTickets = 1, IdUser = 1, IdStatus = 1, SeatNumber = 2 },
                new Ticket() { IdAllTickets = 2, IdUser = 1, IdStatus = 1, SeatNumber = 4 },
                new Ticket() { IdAllTickets = 3, IdUser = 1, IdStatus = 2, SeatNumber = 6 },
                new Ticket() { IdAllTickets = 4, IdUser = 1, IdStatus = 2, SeatNumber = 3 }
            };
            await Context.Tickets.AddRangeAsync(tickets);
            await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}


