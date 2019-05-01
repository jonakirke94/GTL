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
            RuleForEach(x => x.Addresses).SetValidator(new AddressValidator());
            // Must validations only get run on the server
            RuleFor(x => x.Addresses).Must(add => add.Count > 0).WithMessage("A new member must have at least one address");
        }

        public class AddressValidator : AbstractValidator<Address>
        {
            public AddressValidator()
            {
                RuleFor(x => x.StreetName).NotEmpty();
                RuleFor(x => x.City).NotEmpty();
                RuleFor(x => x.ZipCode).NotEmpty();
                RuleFor(x => x.HouseNumber).NotEmpty();
                RuleFor(x => x.AddressType).IsInEnum();
            }
        }
    }
}
