using System;
using System.Collections.Generic;
using System.Text;

namespace GTL.Domain.Entities
{
    public class Loan
    {
        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string MemberSsn { get; set; } 
        public string CopyBarcode { get; set; }
        public string LibraryName { get; set; }
    }
}
