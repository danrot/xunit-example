using Xunit;
using XUnitExample.Domain;

namespace XUnitExample.Tests.Domain
{
    public class EventTest
    {
        [Fact]
        public void CreateEvent()
        {
            // Act
            var event1 = new Event("Event 1");

            // Assert
            Assert.Equal("Event 1", event1.Title);
        }

        [Fact]
        public void RegisterParticipant()
        {
            // Arrange
            var event1 = new Event("Event 1");
            var participant1 = new Participant("Stefan", "Schwärzler");

            // Act
            event1.RegisterParticipant(participant1);

            // Assert
            Assert.Single(event1.Participants);
            Assert.Equal(participant1, event1.Participants[0]);
        }
    }
}
