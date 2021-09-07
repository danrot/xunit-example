using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using XUnitExample.Domain;

namespace XUnitExample.Persistence
{
    public class EventRepository : IEventRepository
    {
        private readonly EventContext _context;

        public EventRepository(EventContext context)
        {
            _context = context;
        }

        public void Add(Event e)
        {
            _context.Events.Add(e);
        }

        public async Task<Event> GetByIdAsync(Guid id)
        {
            return await _context.Events.SingleOrDefaultAsync(e => e.Id == id);
        }
    }
}
