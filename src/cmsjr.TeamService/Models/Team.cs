using System;
using System.Collections.Generic;

namespace cmsjr.TeamService.Models
{
    public class Team
    {
        public Team()
        {
            Members = new List<Member>();
        }

        public Team(string name) : this()
        {
            Name = name;
        }

        public Team(string name, Guid id) : this(name)
        {
            ID = id;
        }

        public string Name { get; set; }
        public Guid ID { get; set; }
        public ICollection<Member> Members { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}