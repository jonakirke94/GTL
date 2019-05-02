using System;
using System.Collections.Generic;
using System.Text;

namespace GTL.Domain.Entities
{
    public class Library
    {
        public string Name { get; set; }
        public int ProfessorLoanDuration { get; set; }
        public int ProfessorGracePeriod { get; set; }
        public int MemberLoanDuration { get; set; }
        public int MemberGracePeriod { get; set; }
        public int ProfessorMaxBooksOnLoan { get; set; }
        public int MemberMaxBooksOnLoan { get; set; }
    }
}
