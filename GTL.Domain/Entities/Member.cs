using System;
using System.Collections.Generic;
using System.Text;

namespace GTL.Domain.Entities
{
    public class Member
    {
        public Member()
        {
            LoanerCards = new List<LoanerCard>();
        }

        public string Ssn { get; set; }

        public IEnumerable<LoanerCard> LoanerCards { get; private set; }
    }
}
