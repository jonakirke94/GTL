using System;
using System.Collections.Generic;
using System.Text;

namespace GTL.Domain.Entities.Identity
{
    public class Role
    {
        public Role(string name)
        {
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string NormalizedName { get; set; }

        public int PermissionLevel { get; set; }
    }
}
