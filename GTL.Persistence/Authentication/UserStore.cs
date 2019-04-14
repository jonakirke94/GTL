using Dapper;
using GTL.Application.Interfaces.Authentication;
using GTL.Domain.Entities;
using GTL.Persistence.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GTL.Persistence.Authentication
{
    public class UserStore : IUserStore
    {
        private DataBaseSettings _options { get; }

        public UserStore(IOptions<DataBaseSettings> optionsAccessor)
        {
            _options = optionsAccessor.Value;
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_options.ConnectionString))
            {
                await connection.OpenAsync(cancellationToken);
                user.Id = await connection.QuerySingleAsync<int>($@"INSERT INTO [User] ([Name], [NormalizedName], [Email],
                [NormalizedEmail], [PasswordHash], [PasswordSalt],  [LastChanged])
                VALUES (@{nameof(User.Name)}, @{nameof(User.NormalizedName)}, @{nameof(User.Email)},
                @{nameof(User.NormalizedEmail)}, @{nameof(User.PasswordHash)}, @{nameof(User.PasswordSalt)}, @{nameof(User.LastChanged)});
                SELECT CAST(SCOPE_IDENTITY() as int)", user);
            }

            return IdentityResult.Success;
        }

        public async Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_options.ConnectionString))
            {
                await connection.OpenAsync(cancellationToken);
                return await connection.QuerySingleOrDefaultAsync<User>($@"SELECT * FROM [User]
                    WHERE [Email] = @{nameof(email)}", new { email });
            }
        }

        public async Task<User> GetUserByIdAsync(int id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_options.ConnectionString))
            {
                await connection.OpenAsync(cancellationToken);
                return await connection.QuerySingleOrDefaultAsync<User>($@"SELECT * FROM [User]
                    WHERE [Id] = @{nameof(id)}", new { id });
            }
        }

        public async Task<IEnumerable<User>> GetUsersAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_options.ConnectionString))
            {
                await connection.OpenAsync(cancellationToken);
                return await connection.QueryAsync<User>($@"SELECT * FROM [User]");
            }
        }
    }
}
