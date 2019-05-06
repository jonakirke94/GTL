using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using GTL.Application.Infrastructure.RequestModels;
using GTL.Domain.Entities;
using MediatR;


namespace GTL.Application.UseCases.Loans.Commands.CreateLoan
{
    public class CreateLoanCommand : IRequest<CommandResponse>
    {
        public Loan Loan { get; set; } = new Loan();
    }

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
