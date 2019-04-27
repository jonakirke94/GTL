using GTL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GTL.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<int> CreateAsync(User user, CancellationToken cancellationToken);

        Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken);

        Task<User> GetUserByIdAsync(int id, CancellationToken cancellationToken);

        Task<IEnumerable<User>> GetUsersAsync(CancellationToken cancellationToken);

        Task UpdateUserAsync(User user, CancellationToken cancellationToken);

        Task DeleteUserAsync(int id, CancellationToken cancellationToken);
    }
}

