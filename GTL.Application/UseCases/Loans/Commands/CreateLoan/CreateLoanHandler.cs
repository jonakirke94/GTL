using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GTL.Application.Interfaces.Repositories;
using GTL.Domain.Entities;
using GTL.Domain.Enums;
using MediatR;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace GTL.Application.UseCases.Loans.Commands.CreateLoan
{
    public class CreateLoanHandler : IRequestHandler<CreateLoanCommand, Unit>
    {
        private ILoanRepository _loanRepo;
        private IMemberRepository _memberRepo;
        private ICopyRepository _copyRepo;
        private ILibraryRepository _libraryRepo;

        public CreateLoanHandler(ILoanRepository loanRepo, IMemberRepository memberRepo, ICopyRepository copyRepo, ILibraryRepository libraryRepo)
        {
            _loanRepo = loanRepo;
            _memberRepo = memberRepo;
            _copyRepo = copyRepo;
            _libraryRepo = libraryRepo;
        }

        public Task<Unit> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
        {
            var member = _memberRepo.GetMemberBySsn(request.Loan.MemberSsn);

            var copy = _copyRepo.GetCopyByBarcode(request.Loan.CopyBarcode);

            if (copy.Status != null || (copy.Status == CopyStatus.IsOnLoan || copy.Status == CopyStatus.Broken) || member is null )
            {
                //TODO cast exception
            }

            Library library = _libraryRepo.GetLibraryByName(request.Loan.LibraryName);

            int loanDuration = 0;
            int gracePeriod;
            int maxBooksOnLoan = 0;

            if (member.type == MemberType.Professor)
            {
                loanDuration = library.ProfessorLoanDuration;
                gracePeriod = library.ProfessorGracePeriod;
                maxBooksOnLoan = library.ProfessorMaxBooksOnLoan;
            }
            else if (member.type == MemberType.Member)
            {
                loanDuration = library.MemberLoanDuration;
                gracePeriod = library.MemberGracePeriod;
                maxBooksOnLoan = library.MemberMaxBooksOnLoan;
            }
            else
            {
                //TODO throw exception "member was not a type."
            }

            int amountOfBooksLoanedByMember = _loanRepo.GetAllActiveLoans(member.Ssn);

            if (amountOfBooksLoanedByMember > maxBooksOnLoan)
            {
                request.Loan.DueDate = request.Loan.LoanDate.AddDays(loanDuration);

                _loanRepo.createLoan(request.Loan);
            }
            else
            {
                //TODO Throw exception "too many books on loan"
            }

           

            return Task.Run(() => Unit.Value);

        }
    }
}
