using System;
using System.Threading;
using System.Threading.Tasks;
using GTL.Application.Exceptions;
using GTL.Application.Infrastructure.RequestModels;
using GTL.Application.Interfaces;
using GTL.Application.Interfaces.Repositories;
using GTL.Application.Interfaces.UnitOfWork;
using GTL.Domain.Entities;
using GTL.Domain.Enums;
using MediatR;

namespace GTL.Application.Features.Loans.Commands.CreateLoan
{
    public class CreateLoanHandler : IRequestHandler<CreateLoanCommand, CommandResponse>
    {
        private readonly ILoanRepository _loanRepo;
        private readonly ICopyRepository _copyRepo;
        private readonly ILoanHelper _loanHelper;
        private readonly IGTLContext _context;

        public CreateLoanHandler(IGTLContext context, ILoanRepository loanRepo, ILoanHelper loanHelper, ICopyRepository copyRepo)
        {
            _loanRepo = loanRepo;
            _loanHelper = loanHelper;
            _copyRepo = copyRepo;
            _context = context;
        }

        public Task<CommandResponse> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
        {
            var response = new CommandResponse();

            var isCardActive = _loanHelper.IsLoanerCardActive(request.LoanerCardBarcode);

            if (!isCardActive)
            {
                response.ErrorMessage = "The used loanercard is no longer active";
                return Task.FromResult(response);
            }

            var activeLoans = _loanRepo.GetNoOfActiveLoans(request.LoanerCardBarcode);

            if (activeLoans > 4 || activeLoans < 0)
            {
                response.ErrorMessage = "Member has reached limit of allowed active loans";
                return Task.FromResult(response);
            }

            var copy = _copyRepo.GetByBarcode(request.CopyBarcode);

            if (copy.Status != CopyStatus.AVAILABLE)
            {
                response.ErrorMessage = $"The requested copy is not available for loaning Status: {copy.Status}";
                return Task.FromResult(response);
            }

            var loanToAdd = new Loan
            {
                LibraryName = request.LibraryName,
                CopyBarcode = request.CopyBarcode,
                LoanerCardBarcode = request.LoanerCardBarcode,
                LoanDate = DateTime.Now,
                DueDate = _loanHelper.GetDueDateByMemberType(request.LoanerCardBarcode, request.LibraryName)
            };


            using (var db = _context.CreateUnitOfWork())
            {
                response.SubjectId = _loanRepo.Add(loanToAdd);
                db.SaveChanges();
            }

            return Task.FromResult(response);
        }
    }
}
