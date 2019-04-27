using Dapper;
using GTL.Application.Interfaces;
using GTL.Application.Interfaces.Repositories;
using GTL.Domain.Entities;
using GTL.Domain.Entities.Identity;
using GTL.Persistence.Configurations;
using GTL.Persistence.Helpers;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GTL.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DataBaseSettings Options { get; }

        public UserRepository(IOptions<DataBaseSettings> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public async Task<int> CreateAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(Options.ConnectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var id = await connection.QuerySingleAsync<int>($@"INSERT INTO [User] ([Name], [NormalizedName], [Email],
                    [NormalizedEmail], [PasswordHash], [PasswordSalt],  [LastChanged], [PermissionLevel])
                    VALUES (@{nameof(User.Name)}, @{nameof(User.NormalizedName)}, @{nameof(User.Email)},
                    @{nameof(User.NormalizedEmail)}, @{nameof(User.PasswordHash)}, @{nameof(User.PasswordSalt)}, @{nameof(User.LastChanged)}, @{nameof(User.PermissionLevel)});
                    SELECT CAST(SCOPE_IDENTITY() as int)", user);

                return id;
            }           
        }

        public async Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            const string query = @"SELECT * FROM [User] WHERE Email = @email;";

            using (var connection = new SqlConnection(Options.ConnectionString))
            {
                var users = await connection.QueryAsync<User>(query, new { email });
                return users.FirstOrDefault();
            }


            // MANY TO MANY EXAMPLE
            //Dictionary<int, User> result = new Dictionary<int, User>();

            //string query = @"SELECT u.*, r.* FROM [User] u JOIN [UserRole] ur on u.Id = ur.UserId JOIN [Role] r on ur.RoleId = r.Id WHERE u.Email = @email;";
            //using (var connection = new SqlConnection(_options.ConnectionString))
            //{
            //    var users = await connection.QueryAsync<User, Role, User>(query, (u, r) =>
            //    {
            //        // this lambda is called for each record retrieved by Dapper
            //        // receiving a user and a role created by Dapper from the record
            //        // and it is expected to return a user.
            //        // We look if the user passed in is already in the dictionary 
            //        // and add the role received to the roles list of that user
            //        if (!result.ContainsKey(u.Id))
            //            result.Add(u.Id, u);
            //        User working = result[u.Id];
            //        working.Role = r;
            //        return u;
            //    }, new {email});

            //    // Return the first element in the dictionary
            //    if (result.Values.Count > 0)
            //        return result.Values.First();
            //    else
            //        return null;
            //}
        }

        public async Task<User> GetUserByIdAsync(int id, CancellationToken cancellationToken)
        {
                cancellationToken.ThrowIfCancellationRequested();
                const string query = @"SELECT * FROM [User] WHERE Id = @id;";
                using (var connection = new SqlConnection(Options.ConnectionString))
                {
                    var user = await connection.QueryAsync<User>(query, new { id });
                    return user.FirstOrDefault();
                }
        }

        public async Task<IEnumerable<User>> GetUsersAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            const string query = @"SELECT * FROM [User]";
            using (var connection = new SqlConnection(Options.ConnectionString))
            {
                var users = await connection.QueryAsync<User>(query);

                return users.Any() ? users : new List<User>();
            }
        }

        public Task UpdateUserAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
