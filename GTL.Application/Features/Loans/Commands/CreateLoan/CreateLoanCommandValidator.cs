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
            RuleFor(x => x.CopyBarcode).NotEmpty();
            RuleFor(x => x.LoanerCardBarcode).NotEmpty();
            RuleFor(x => x.LibraryName).NotEmpty();

        }
    }
}
