using System;
using System.Collections.Generic;
using System.Text;

namespace GTL.Application.Exceptions
{
    public class MissingPermissionException : Exception
    {
        public MissingPermissionException(string name)
            : base($"Creating of entity \"{name}\" failed. A user must be created with a permission level")
        {
        }
    }
}
