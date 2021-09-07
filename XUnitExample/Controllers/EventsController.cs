using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using XUnitExample.Domain;
using XUnitExample.Persistence;

namespace XUnitExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventRepository _repository;

        private readonly EventContext _context;

        public EventsController(IEventRepository repository, EventContext context)
        {
            _repository = repository;
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(Guid id)
        {
            var @event = await _repository.GetByIdAsync(id);

            if (@event == null)
            {
                return NotFound();
            }

            return @event;
        }

        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent(Event @event)
        {
            _repository.Add(@event);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvent", new { id = @event.Id }, @event);
        }
    }
}
