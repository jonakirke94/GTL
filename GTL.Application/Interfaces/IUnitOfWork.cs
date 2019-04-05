using GTL.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTL.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository _userRepo { get; }

        void Commit();
    }
}
