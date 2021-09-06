using XUnitExample.Domain;
using Microsoft.EntityFrameworkCore;

namespace XUnitExample.Persistence
{
    public sealed class EventContext : DbContext
    {
        public DbSet<Event> Events => Set<Event>();

        public EventContext(DbContextOptions<EventContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Participant>()
                .HasKey(p => p.Id);
        }
    }
}
