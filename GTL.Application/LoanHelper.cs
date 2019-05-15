using System;
using System.Collections.Generic;
using System.Text;
using GTL.Application.Interfaces;
using GTL.Application.Interfaces.Repositories;
using GTL.Domain.Enums;

namespace GTL.Application
{
    public class LoanHelper : ILoanHelper
    {
        private readonly ILoanerCardRepository _loanerCardRepo;
        private readonly IMemberRepository _memberRepo;
        private readonly ILibraryRepository _libraryRepo;

        public LoanHelper(ILoanerCardRepository loanerCardRepo, IMemberRepository memberRepo, ILibraryRepository libraryRepo)
        {
            _loanerCardRepo = loanerCardRepo;
            _memberRepo = memberRepo;
            _libraryRepo = libraryRepo;
        }

        public bool IsLoanerCardActive(int barcode)
        {
            var loanerCard = _loanerCardRepo.GetByBarcode(barcode);
            return loanerCard.IsActive;
        }

        public DateTime GetDueDateByMemberType(int barcode, string libraryName)
        {
            var member = _memberRepo.GetByLoanerCard(barcode);

            var library = _libraryRepo.GetByName(libraryName);

            return member.Type == MemberType.STUDENT
                ? DateTime.Now.AddDays(library.MemberLoanDuration)
                : DateTime.Now.AddDays(library.ProfessorLoanDuration);
        }
    }
}
