using System;

namespace XUnitExample
{
    public class Participant
    {
        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public Participant(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
