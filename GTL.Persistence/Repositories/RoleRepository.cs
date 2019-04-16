using Dapper;
using GTL.Application.Interfaces.Repositories;
using GTL.Domain.Entities.Identity;
using GTL.Persistence.Configurations;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GTL.Persistence.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private DataBaseSettings Options { get; }

        public RoleRepository(IOptions<DataBaseSettings> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            string query = @"SELECT * FROM [Role]";
            using (var connection = new SqlConnection(Options.ConnectionString))
            {
                return await connection.QueryAsync<Role>(query);
            }
        }

        public Task<Role> GetRoleByNameAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}

