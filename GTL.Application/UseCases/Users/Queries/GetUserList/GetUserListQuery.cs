using GTL.Application.ViewModels;
using MediatR;

namespace GTL.Application.UseCases.Users.Queries.GetUserList
{
    public class GetUserListQuery : IRequest<UserListViewModel>
    {
    }
}
