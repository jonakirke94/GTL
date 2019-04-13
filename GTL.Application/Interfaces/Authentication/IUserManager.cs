using GTL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GTL.Application.Interfaces.Authentication
{
    public interface IUserManager
    {
        Task CreateAsync(User user, CancellationToken cancellationToken);
    }
}
