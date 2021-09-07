using System;
using System.Threading.Tasks;

namespace XUnitExample.Domain
{
    public interface IEventRepository
    {
        public void Add(Event e);

        public Task<Event> GetByIdAsync(Guid id);
    }
}
