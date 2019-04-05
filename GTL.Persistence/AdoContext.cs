using GTL.Application.Interfaces;
using GTL.Persistence.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTL.Persistence
{
    public class AdoContext : IAdoContext
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private readonly bool _ownConnection;
        private DataBaseSettings _options { get; }

        public AdoContext(IOptions<DataBaseSettings> optionsAccessor)
        {
            _options = optionsAccessor.Value;            
            _connection = new SqlConnection(_options.ConnectionString);
            _connection.Open();

            _ownConnection = _options.OwnConnection;

            _transaction = _connection.BeginTransaction();
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction = null;
            }
            if (_connection != null && _ownConnection)
            {
                _connection.Close();
                _connection = null;
            }
        }

        public IDbCommand CreateCommand()
        {
            var command = _connection.CreateCommand();
            command.Transaction = _transaction;
            return command;
        }

        public void SaveChanges()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("Transaction has already been already been commited. Check your transaction handling.");
            }
            _transaction.Commit();
            _transaction = null;

        }
    }
}
