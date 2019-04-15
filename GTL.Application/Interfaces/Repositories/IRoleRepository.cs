using GTL.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GTL.Application.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        Task AddRolesToUser(List<UserRole> userRoles, CancellationToken cancellationToken);

        Task<IEnumerable<Role>> GetAllRolesAsync(CancellationToken cancellationToken);
    }
}

