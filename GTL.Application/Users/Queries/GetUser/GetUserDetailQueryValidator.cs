using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace GTL.Application.Users.Queries.GetUser
{
    public class GetUserDetailQueryValidator : AbstractValidator<GetUserDetailQuery>
    {
        public GetUserDetailQueryValidator()
        {
            RuleFor(v => v.Id).NotEmpty();
        }
    }
}
