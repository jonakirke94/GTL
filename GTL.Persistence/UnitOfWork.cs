using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTL.Application.Interfaces;
using GTL.Application.Interfaces.Repositories;

namespace GTL.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IAdoContext _context;
        public IUserRepository _userRepo { get; }

        public UnitOfWork(IAdoContext context, IUserRepository userRepo)
        {
            _context = context;
            _userRepo = userRepo;
        }


        public void Commit()
        {
            _context.SaveChanges();
        }

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
