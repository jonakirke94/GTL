using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTL.Web.Models
{
    public class LoginViewModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public bool isPersistent { get; set; }
    }
}
