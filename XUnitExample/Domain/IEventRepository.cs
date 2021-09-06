using System;

namespace XUnitExample.Domain
{
    public interface IEventRepository
    {
        public void Add(Event e);

        public Event GetById(Guid id);
    }
}
