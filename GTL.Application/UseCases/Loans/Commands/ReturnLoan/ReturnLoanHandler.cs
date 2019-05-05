using GTL.Application.Interfaces.Repositories;
using GTL.Application.Interfaces.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GTL.Application.UseCases.Loans.Commands.ReturnLoan
{
   public class ReturnLoanHandler : IRequestHandler<ReturnLoanCommand, Unit>
    {
        private readonly ILoanRepository _loanRepo;
        private readonly IGTLContext _context;

        public ReturnLoanHandler(IGTLContext context, ILoanRepository loanRepo)
        {
            _loanRepo = loanRepo;
            _context = context;
        }

        public Task<Unit> Handle(ReturnLoanCommand request, CancellationToken cancellationToken)
        {
            _loanRepo.Return(request.CopyBarcode);

            return Task.Run(() => Unit.Value);
        }
    }
}
