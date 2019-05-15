using GTL.Application.Infrastructure.RequestModels;
using GTL.Domain.Entities;
using MediatR;

namespace GTL.Application.Features.Loans.Commands.CreateLoan
{
    public class CreateLoanCommand : IRequest<CommandResponse>
    {
        public string LoanerCardBarcode { get; set; }

        public string CopyBarcode { get; set; }

        public string LibraryName { get; set; }
    }
}
