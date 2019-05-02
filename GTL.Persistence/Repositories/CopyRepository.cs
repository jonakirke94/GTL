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
    public class CopyRepository : ICopyRepository
    {
        private DataBaseSettings Options { get; }

        public CopyRepository(IOptions<DataBaseSettings> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public Copy GetCopyByBarcode(string barcode)
        {
            var query = $@"SELECT * FROM Copy WHERE Barcode = @barcode";
            using (var connection = new SqlConnection(Options.ConnectionString))
            {
                connection.Open();
                var copy = connection.ExecuteScalar<Copy>(query, new { barcode });
                return copy;
            }
        }
    }
}
