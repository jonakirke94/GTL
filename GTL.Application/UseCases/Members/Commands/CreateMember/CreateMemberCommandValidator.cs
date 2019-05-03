using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using GTL.Domain.Entities;

namespace GTL.Application.UseCases.Members.Commands.CreateMember
{
    public class CreateMemberCommandValidator : AbstractValidator<CreateMemberCommand>
    {
        public CreateMemberCommandValidator()
        {
            RuleFor(x => x.Ssn).NotEmpty().Length(10);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.StreetName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.City).NotEmpty().MaximumLength(30);
            RuleFor(x => x.ZipCode).NotEmpty().MaximumLength(8);
            RuleFor(x => x.HouseNumber).NotEmpty().MaximumLength(10);
            RuleFor(x => x.AddressType).IsInEnum();
        }
    }
}
