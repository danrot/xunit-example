using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading.Tasks;
using Xunit;
using XUnitExample.Controllers;
using XUnitExample.Domain;
using XUnitExample.Persistence;

namespace XUnitExample.Tests.Controllers
{
    public class EventsControllerUnitTest
    {
        private EventsController _controller;

        private Mock<EventContext> _context;

        private Mock<IEventRepository> _repository;

        public EventsControllerUnitTest()
        {
            _repository = new Mock<IEventRepository>();
            _context = new Mock<EventContext>(new DbContextOptions<EventContext>());
            _controller = new EventsController(_repository.Object, _context.Object);
        }

        [Fact]
        public async void GetEvent()
        {
            var @event = new Event("Title");
            _repository.Setup(repository => repository.GetByIdAsync(@event.Id)).Returns(Task.FromResult(@event));

            var response = await _controller.GetEvent(@event.Id);

            Assert.Equal(@event.Id, response.Value.Id);
            Assert.Equal("Title", response.Value.Title);
        }

        [Fact]
        public async void PostEvent()
        {
            var @event = new Event("Title");
            var response = await _controller.PostEvent(@event);

            _repository.Verify(repository => repository.Add(@event));
            _context.Verify(context => context.SaveChangesAsync(default));
        }
    }
}
