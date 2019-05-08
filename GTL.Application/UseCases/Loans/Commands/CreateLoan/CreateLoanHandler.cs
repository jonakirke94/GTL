using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GTL.Application.Exceptions;
using GTL.Application.Infrastructure.RequestModels;
using GTL.Application.Interfaces.Repositories;
using GTL.Application.Interfaces.UnitOfWork;
using GTL.Domain.Entities;
using GTL.Domain.Enums;
using MediatR;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace GTL.Application.UseCases.Loans.Commands.CreateLoan
{
    public class CreateLoanHandler : IRequestHandler<CreateLoanCommand, CommandResponse>
    {
        private readonly ILoanRepository _loanRepo;
        private readonly IMemberRepository _memberRepo;
        private readonly ICopyRepository _copyRepo;
        private readonly ILibraryRepository _libraryRepo;
        private readonly IGTLContext _context;

        public CreateLoanHandler(IGTLContext context, ILoanRepository loanRepo, IMemberRepository memberRepo, ICopyRepository copyRepo, ILibraryRepository libraryRepo)
        {
            _loanRepo = loanRepo;
            _memberRepo = memberRepo;
            _copyRepo = copyRepo;
            _libraryRepo = libraryRepo;
            _context = context;
        }

        public Task<CommandResponse> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
        {
            var response = new CommandResponse();

            //var member = _memberRepo.GetBySsn(request.Loan.MemberSsn);

            //var copy = _copyRepo.GetCopyByBarcode(request.Loan.CopyBarcode);

            using (var db = _context.CreateUnitOfWork())
            {
                try
                {
                    _loanRepo.Add(request.Loan);
                    db.SaveChanges();

                }
                catch (NotAllowedForLoan)
                {
                    response.ErrorMessage = "Warning. The requested copy is not available for loaning";
                    return Task.FromResult(response);
                }
            }
            


            /*        var member = _memberRepo.GetBySsn(request.Loan.MemberSsn);

                    var copy = _copyRepo.GetCopyByBarcode(request.Loan.CopyBarcode);

                    Library library = _libraryRepo.GetLibraryByName(request.Loan.LibraryName);

             /*       if ((copy.Status != CopyStatus.AVAILABLE) || member is null || library.Name != null)
                    {
                        //TODO cast exception
                    } 


                    //TODO maybe not set theese to null.
                    int loanDuration = 0;
                    int gracePeriod = 0;
                    int maxBooksOnLoan = 0;

                    if (member.Type == MemberType.PROFESSOR)
                    {
                        loanDuration = library.ProfessorLoanDuration;
                        gracePeriod = library.ProfessorGracePeriod;
                        maxBooksOnLoan = library.ProfessorMaxBooksOnLoan;
                    }
                    else if (member.Type == MemberType.STUDENT)
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

                    if (amountOfBooksLoanedByMember < maxBooksOnLoan)
                    {
                        request.Loan.DueDate = request.Loan.LoanDate.AddDays(loanDuration);

                        _loanRepo.Add(request.Loan);
                    }
                    else if (maxBooksOnLoan == 0)
                    {
                        request.Loan.DueDate = request.Loan.LoanDate.AddDays(loanDuration);

                        _loanRepo.Add(request.Loan);
                    }
                    else
                    {
                        //TODO Throw exception "too many books on loan"
                    } */

            




            return Task.FromResult(response);

        }
    }
}
