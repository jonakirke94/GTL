using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using GTL.Application.Interfaces.Repositories;
using GTL.Domain.Entities;
using GTL.Persistence.Configurations;
using Microsoft.Extensions.Options;

namespace GTL.Persistence.Repositories
{
    public class LibraryRepository : ILibraryRepository
    {
        private DataBaseSettings Options { get; }

        public LibraryRepository(IOptions<DataBaseSettings> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public Library GetLibraryByName(string name)
        {
            var query = $@"SELECT * FROM Library WHERE Name = @name";
            using (var connection = new SqlConnection(Options.ConnectionString))
            {
                connection.Open();
                var library = connection.ExecuteScalar<Library>(query, new { name });
                return library;
            }
        }
    }
}
