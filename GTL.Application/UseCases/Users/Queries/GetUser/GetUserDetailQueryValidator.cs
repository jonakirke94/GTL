using FluentValidation;
using GTL.Application.Users.Queries.GetUser;

namespace GTL.Application.UseCases.Users.Queries.GetUser
{
    public class GetUserDetailQueryValidator : AbstractValidator<GetUserDetailQuery>
    {
        public GetUserDetailQueryValidator()
        {
            RuleFor(v => v.Id).NotEmpty();
        }
    }
}
