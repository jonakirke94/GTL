using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using GTL.Application.Interfaces.Repositories;
using GTL.Application.Interfaces.UnitOfWork;
using GTL.Domain.Entities;
using GTL.Persistence.Configurations;

namespace GTL.Persistence.Repositories
{
    public class LibraryRepository : ILibraryRepository
    {
        protected readonly IGTLContext _context;

        public LibraryRepository(IGTLContext context)
        {
            _context = context;
        }

        public Library GetByName(string name)
        {
            const string query = @"SELECT * FROM Library WHERE Name = @name";
            using (var cmd = _context.CreateCommand())
            {
                var para = new DynamicParameters();
                para.Add("@name", name);
                var results = cmd.Connection.Query<Library>(query, para, cmd.Transaction);
                return results.FirstOrDefault();
            }
        }
    }
}
