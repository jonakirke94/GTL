using System;
using System.Collections.Generic;
using System.Text;
using GTL.Domain.Enums;

namespace GTL.Domain.Entities
{
    public class Copy
    {
        public string Barcode { get; set; }

        public CopyStatus Status;
        public int MaterialId { get; set; }
    }
}
