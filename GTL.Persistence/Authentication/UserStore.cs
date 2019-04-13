using GTL.Application.Interfaces.Authentication;
using GTL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GTL.Persistence.Authentication
{
    public class UserStore : IUserStore
    {
        public Task CreateAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
