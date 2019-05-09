using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace GTL.Application.Features.Loans.Commands.CreateLoan
{
    public class CreateLoanCommandValidator : AbstractValidator<CreateLoanCommand>
    {
        public CreateLoanCommandValidator()
        {
            RuleFor(x => x.Loan.MemberSsn).NotEmpty().Length(10);
            RuleFor(x => x.Loan.CopyBarcode).NotEmpty();
            RuleFor(x => x.Loan.LoanDate).NotEmpty();
            RuleFor(x => x.Loan.LibraryName).NotEmpty();
        }
    }
}
