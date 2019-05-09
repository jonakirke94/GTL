using GTL.Application.Infrastructure.RequestModels;
using GTL.Domain.Entities;
using MediatR;

namespace GTL.Application.Features.Loans.Commands.CreateLoan
{
    public class CreateLoanCommand : IRequest<CommandResponse>
    {
        public Loan Loan { get; set; } = new Loan();
    }
}
