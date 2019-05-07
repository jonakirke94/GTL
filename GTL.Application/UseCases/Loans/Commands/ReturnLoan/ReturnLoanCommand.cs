using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GTL.Application.UseCases.Loans.Commands.ReturnLoan
{
    public class ReturnLoanCommand : IRequest
    {
        public string CopyBarcode { get; set; }
    }

    public class ReturnLoanCommandValidator : AbstractValidator<ReturnLoanCommand>
    {
        public ReturnLoanCommandValidator()
        {
            RuleFor(x => x.CopyBarcode).NotEmpty();
        }
    }
}
