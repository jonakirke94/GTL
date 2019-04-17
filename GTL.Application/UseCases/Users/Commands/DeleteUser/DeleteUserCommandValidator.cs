using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using GTL.Application.UseCases.Users.Commands.DeleteUser;

namespace GTL.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
