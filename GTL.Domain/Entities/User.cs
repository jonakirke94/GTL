using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTL.Domain.Entities
{
    public class User
    {
        // Always set collections setter to private and instantiate it in the constructor 

        public User()
        {
            Books = new List<Book>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }

        public ICollection<Book> Books { get; private set; }
    }
}

