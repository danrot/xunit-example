using Microsoft.EntityFrameworkCore;
using System;
using Xunit;
using XUnitExample.Domain;
using XUnitExample.Persistence;

namespace XUnitExample.Tests.Persistence
{
    public class EventRepositoryTest : IDisposable
    {
        private static readonly string _connectionString =
            $"server=localhost;user={Environment.GetEnvironmentVariable("MYSQL_USER") ?? "root"};"
            + $"password={Environment.GetEnvironmentVariable("MYSQL_PASSWORD") ?? ""};"
            + $"database={Environment.GetEnvironmentVariable("MYSQL_DATABASE") ?? "Event_Test"}";

        private EventContext _context;

        private readonly DbContextOptions<EventContext> _options = new DbContextOptionsBuilder<EventContext>()
            .UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString))
            .Options;

        public EventRepositoryTest()
        {
            _context = new EventContext(_options);

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public async void GetByNonExistingId()
        {
            using (var context = new EventContext(_options))
            {
                var repository = new EventRepository(context);

                Assert.Null(await repository.GetByIdAsync(Guid.NewGuid()));
            }
        }

        [Fact]
        public async void GetById()
        {
            Guid eventId1;

            using (var context = new EventContext(_options))
            {
                var repository = new EventRepository(context);
                var event1 = new Event("Test Event");
                eventId1 = event1.Id;

                repository.Add(event1);
                context.SaveChanges();
            }

            using (var context = new EventContext(_options))
            {
                var repository = new EventRepository(context);

                var event1 = await repository.GetByIdAsync(eventId1);

                Assert.Equal("Test Event", event1.Title);
            }
        }
    }
}
