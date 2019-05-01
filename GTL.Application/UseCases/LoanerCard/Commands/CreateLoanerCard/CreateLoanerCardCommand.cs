using FluentValidation;
using GTL.Application.Interfaces.Authorization;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GTL.Application.UseCases.LoanerCard.Commands.CreateLoanerCard
{
    public class CreateLoanerCardCommand : IRequest //IChiefLibrarianRequest
    {
        public string Ssn { get; set; }
    }

    public class CreateLoanerCardCommandValidator : AbstractValidator<CreateLoanerCardCommand>
    {
        public CreateLoanerCardCommandValidator()
        {
            RuleFor(x => x.Ssn).NotEmpty();
        }
    }
}
