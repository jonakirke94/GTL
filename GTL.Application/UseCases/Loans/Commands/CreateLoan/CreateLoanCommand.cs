using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using GTL.Domain.Entities;
using MediatR;


namespace GTL.Application.UseCases.Loans.Commands.CreateLoan
{
    public class CreateLoanCommand : IRequest
    {
        public Loan Loan { get; set; } = new Loan();
    }

    public class CreateLoanCommandValidator : AbstractValidator<CreateLoanCommand>
    {
        public CreateLoanCommandValidator()
        {
            RuleFor(x => x.Loan.MemberSsn).NotEmpty();
            RuleFor(x => x.Loan.MemberSsn).MinimumLength(11);
            RuleFor(x => x.Loan.MemberSsn).MaximumLength(11);
            RuleFor(x => x.Loan.CopyBarcode).NotEmpty();
            RuleFor(x => x.Loan.LoanDate).NotEmpty();
            RuleFor(x => x.Loan.LibraryName).NotEmpty();
        }
    }
}
