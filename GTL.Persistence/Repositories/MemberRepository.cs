using System;
using System.Collections.Generic;
using System.Text;
using GTL.Application.Interfaces.Repositories;
using GTL.Application.Interfaces.UnitOfWork;
using GTL.Domain.Entities;
using GTL.Persistence.Configurations;

namespace GTL.Persistence.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        protected readonly IGTLContext _context;

        private DataBaseSettings Options { get; }

        public MemberRepository(IGTLContext context)
        {
            _context = context;
        }
        public Member GetByLoanerCard(string barcode)
        {
            throw new NotImplementedException();
        }
    }
}
