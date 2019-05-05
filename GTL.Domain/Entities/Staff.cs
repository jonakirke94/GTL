using System;
using System.Collections.Generic;
using System.Text;
using GTL.Domain.Enums;

namespace GTL.Domain.Entities
{
    public class Staff
    {
        public string Ssn { get; set; }

        public Role Role { get; set; }
    }
}
