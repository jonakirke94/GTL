using GTL.Application.Interfaces.UnitOfWork;
using GTL.Persistence.Configurations;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace GTL.Persistence.UnitOfWork
{
    public class ConnectionFactory : IConnectionFactory
    {
        private DataBaseSettings Options { get; }

        public ConnectionFactory(IOptions<DataBaseSettings> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }
        public IDbConnection Create()
        {
            var connection = new SqlConnection {ConnectionString = Options.ConnectionString};

            connection.Open();
            return connection;
        }
    }
}
