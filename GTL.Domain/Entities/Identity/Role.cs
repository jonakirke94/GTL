using System;
using System.Collections.Generic;
using System.Text;

namespace GTL.Domain.Entities.Identity
{
    public class Role
    {
        public Role()
        {
            Users = new List<User>();
        }

        public Role(string name)
        {
            Users = new List<User>();
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string NormalizedName { get; set; }

        public ICollection<User> Users { get; private set; }

    }
}
