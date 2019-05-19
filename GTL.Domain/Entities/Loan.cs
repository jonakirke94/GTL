using System;
using System.Collections.Generic;
using System.Text;

namespace GTL.Domain.Entities
{
    public class Loan
    {
        public int Id { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int LoanerCardBarcode { get; set; } 
        public int CopyBarcode { get; set; }
        public string LibraryName { get; set; }
    }
}
