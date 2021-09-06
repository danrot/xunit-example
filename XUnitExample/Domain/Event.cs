using System;
using System.Collections.Generic;

namespace XUnitExample.Domain
{
    public class Event
    {
        public Guid Id { get; } = Guid.NewGuid();

        public string Title { get; private set; }

        public List<Participant> Participants { get; } = new List<Participant>();

        public Event(string title)
        {
            Title = title;
        }

        public void RegisterParticipant(Participant participant)
        {
            Participants.Add(participant);
        }
    }
}
