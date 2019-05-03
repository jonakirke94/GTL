using GTL.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GTL.Application.Helper.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class Authorize : Attribute
    {
        public Role Role { get; }
        public Authorize(Role role)
        {
            Role = role;
        }
    }
}
