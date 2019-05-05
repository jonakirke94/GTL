using GTL.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTL.Domain.Enums;

namespace GTL.Domain.Entities
{
    public class Material
    {
        public ISBN ISBN { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Edition { get; set; }

        public MaterialType Type { get; set; }
        public string Area { get; set; }
        public string Size { get; set; }

        public DateTime DeletedAt { get; set; }

        public List<Author> Authors { get; set; }

        public List<Subject> Subjects { get; set; }

        public Publisher Publisher { get; set; }

    }
}

