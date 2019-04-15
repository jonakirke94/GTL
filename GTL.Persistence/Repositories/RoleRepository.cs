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
        private DataBaseSettings _options { get; }

        public RoleRepository(IOptions<DataBaseSettings> optionsAccessor)
        {
            _options = optionsAccessor.Value;
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            string query = @"SELECT * FROM [Role]";
            using (var connection = new SqlConnection(_options.ConnectionString))
            {
                return await connection.QueryAsync<Role>(query);
            }
        }

        public async Task AddRoleToUser(List<UserRole> userRoles, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var query = $@"INSERT INTO [UserRole] ([UserId], [RoleId]) VALUES (@UserId, @RoleId);";
            using (var connection = new SqlConnection(_options.ConnectionString))
            {
                await connection.OpenAsync(cancellationToken);
                await connection.ExecuteAsync(query, userRoles);
            }
        }
    }
}
