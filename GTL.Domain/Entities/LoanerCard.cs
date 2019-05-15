using System;
using System.Collections.Generic;
using System.Text;

namespace GTL.Domain.Entities
{
    public class LoanerCard
    {
        public int Barcode { get; set; }
        public string MemberSsn { get; set; }
        public bool IsActive { get; set; }
        public DateTime IssueDate { get; set; }
    }
}
