using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using GTL.Application.Interfaces.Repositories;
using GTL.Domain.Entities;
using GTL.Persistence.Configurations;
using Microsoft.Extensions.Options;
using GTL.Application.Interfaces.UnitOfWork;
using System.Linq;

namespace GTL.Persistence.Repositories
{
    public class LibraryRepository : ILibraryRepository
    {
        protected readonly IGTLContext Context;

        public LibraryRepository(IGTLContext context)
        {
            Context = context;
        }

        public Library GetLibraryByName(string name)
        {
            var query = $@"SELECT * FROM Library WHERE Name = @name";
            using (var cmd = Context.CreateCommand())
            {
                var param = new DynamicParameters();
                param.Add("@name", name);
                var library = cmd.Connection.Query<Library>(query, param, cmd.Transaction);
                return library.FirstOrDefault();
            }
        }
    }
}
