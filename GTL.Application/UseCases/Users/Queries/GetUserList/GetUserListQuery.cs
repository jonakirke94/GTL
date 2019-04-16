using GTL.Application.Authorization;
using GTL.Application.Authorization.Permissions;
using MediatR;

namespace GTL.Application.UseCases.Users.Queries.GetUserList
{
    public class GetUserListQuery : IRequest<UserListDto>
    {
    }
}
