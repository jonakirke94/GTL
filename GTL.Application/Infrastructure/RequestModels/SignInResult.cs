using GTL.Application.Authorization;
using GTL.Domain.Entities;

namespace GTL.Application.Infrastructure.RequestModels
{
    public class SignInResult : CommandResponse
    {
        public User User { get; set; }
        public bool SuccessfulLogin { get; set; }
    }
}
