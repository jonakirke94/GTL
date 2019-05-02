using System;
using System.Collections.Generic;
using System.Text;
using GTL.Domain.Enums;

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
        public string Name { get; set; }
        public string Email { get; set; }
        public MemberType type;
    }
}
