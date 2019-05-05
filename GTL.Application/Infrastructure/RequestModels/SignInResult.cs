using GTL.Domain.Entities;

namespace GTL.Application.Infrastructure.RequestModels
{
    public class SignInResult : CommandResponse
    {
        public Staff Staff { get; set; }
        public bool SuccessfulLogin { get; set; }
    }
}
