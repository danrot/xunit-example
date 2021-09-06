using System;

namespace XUnitExample.Domain
{
    public class Participant
    {
        public Guid Id { get; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public Participant(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
