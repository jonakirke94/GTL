using GTL.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTL.Domain.Entities
{
    public class Book
    { 
        public int Id { get; set; }

        public ISBN ISBN { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public int UserId { get; set; }
    }
}

