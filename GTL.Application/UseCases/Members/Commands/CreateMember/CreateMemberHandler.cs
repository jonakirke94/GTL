using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GTL.Application.Exceptions;
using GTL.Application.Interfaces.Repositories;
using GTL.Application.UseCases.LoanerCard.Commands.CreateLoanerCard;
using GTL.Domain.Entities;
using GTL.Domain.Enums;
using MediatR;

namespace GTL.Application.UseCases.Members.Commands.CreateMember
{
    public class CreateMemberHandler : IRequestHandler<CreateMemberCommand, Unit>
    {
        private readonly IMemberRepository _memberRepo;
        private readonly ILoanerCardRepository _loanerCardRepo;
        private readonly IAddressRepository _addressRepo;

        public CreateMemberHandler(IMemberRepository memberRepo, ILoanerCardRepository loanerCardRepo, IAddressRepository addressRepo)
        {
            _memberRepo = memberRepo;
            _loanerCardRepo = loanerCardRepo;
            _addressRepo = addressRepo;
        }

        public Task<Unit> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
        {
            var existingMember = _memberRepo.GetMemberBySsn(request.Ssn);

            if (existingMember != null)
            {
                throw new NotUniqueSsnException(request.Ssn);
            }

            var member = new Member
            {
                Ssn = request.Ssn,
                Email = request.Email,
                Name = request.Name,
                Type = MemberType.Member,
            };

            var loanerCard = new Domain.Entities.LoanerCard
            {
                IsActive = true,
                IssueDate = DateTime.Now,
                MemberSsn = request.Ssn
            };

            request.Address.MemberSsn = request.Ssn;

            //TODO should be put inside a transaction when we have set up IUnitOfWork
            _addressRepo.AddAddress(request.Address);
            _memberRepo.CreateMember(member);
            _loanerCardRepo.CreateLoanerCard(loanerCard);

            return Task.Run(() => Unit.Value, cancellationToken);
        }
    }
}
