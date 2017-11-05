using System;

namespace cmsjr.TeamService.Models
{
    public class Member
    {
        public Member()
        {
        }

        public Member(Guid id) : this()
        {
            ID = id;
        }


        public Member(Guid id, string firstName, string lastName) : this(id)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public override string ToString()
        {
            return LastName;
        }
    }
}