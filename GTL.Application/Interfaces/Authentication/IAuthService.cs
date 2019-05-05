using System.Threading;
using System.Threading.Tasks;
using GTL.Application.Infrastructure.RequestModels;
using GTL.Domain.Entities;

namespace GTL.Application.Interfaces.Authentication
{
    public interface IPasswordHelper
    {
        bool ValidatePassword(string password, string passwordSalt, string passwordHash);
    }
}


