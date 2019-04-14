using GTL.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GTL.Application.Interfaces.Authentication
{
    public interface IUserStore
    {
        Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken);

        Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken);

        Task<User> GetUserByIdAsync(int id, CancellationToken cancellationToken);

        Task<IEnumerable<User>> GetUsersAsync(CancellationToken cancellationToken);
    }
}
