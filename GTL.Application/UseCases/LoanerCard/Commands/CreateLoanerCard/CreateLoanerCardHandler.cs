using GTL.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GTL.Domain.Entities;

namespace GTL.Application.UseCases.LoanerCard.Commands.CreateLoanerCard
{
    public class CreateLoanerCardHandler : IRequestHandler<CreateLoanerCardCommand, Unit>
    {
        private readonly ILoanerCardRepository _loanerCardRepo;

        public CreateLoanerCardHandler(ILoanerCardRepository loanerCardRepo)
        {
            _loanerCardRepo = loanerCardRepo;
        }

        public Task<Unit> Handle(CreateLoanerCardCommand request, CancellationToken cancellationToken)
        {
            var existingCards = _loanerCardRepo.GetLoanerCardsBySsn(request.Ssn);

            var activeCard = existingCards.SingleOrDefault(x => x.IsActive);

            if (activeCard != null)
            {
                _loanerCardRepo.DeactiveLoanerCard(activeCard.Barcode);
            }

            Domain.Entities.LoanerCard newLoanerCard = new Domain.Entities.LoanerCard
            {
                MemberSsn = request.Ssn,
                IsActive = true,
                IssueDate = DateTime.Now
            };

            _loanerCardRepo.CreateLoanerCard(newLoanerCard);

            return Task.Run(() => Unit.Value);
        }
    }
}
