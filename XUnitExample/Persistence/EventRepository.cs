using System;
using System.Linq;
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

        public Event GetById(Guid id)
        {
            return _context.Events.SingleOrDefault(e => e.Id == id);
        }
    }
}
