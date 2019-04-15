using Dapper;
using GTL.Application.Interfaces.Authentication;
using GTL.Domain.Entities;
using GTL.Domain.Entities.Identity;
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
    //public class UserStore : IUserStore
    //{
    //    private DataBaseSettings _options { get; }

    //    public UserStore(IOptions<DataBaseSettings> optionsAccessor)
    //    {
    //        _options = optionsAccessor.Value;
    //    }

    //    public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
    //    {
    //        cancellationToken.ThrowIfCancellationRequested();

    //        using (var connection = new SqlConnection(_options.ConnectionString))
    //        {
    //            await connection.OpenAsync(cancellationToken);
    //            user.Id = await connection.QuerySingleAsync<int>($@"INSERT INTO [User] ([Name], [NormalizedName], [Email],
    //            [NormalizedEmail], [PasswordHash], [PasswordSalt],  [LastChanged])
    //            VALUES (@{nameof(User.Name)}, @{nameof(User.NormalizedName)}, @{nameof(User.Email)},
    //            @{nameof(User.NormalizedEmail)}, @{nameof(User.PasswordHash)}, @{nameof(User.PasswordSalt)}, @{nameof(User.LastChanged)});
    //            SELECT CAST(SCOPE_IDENTITY() as int)", user);
    //        }

    //        return IdentityResult.Success;
    //    }

    //    public async Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
    //    {
    //        cancellationToken.ThrowIfCancellationRequested();

    //        Dictionary<int, User> result = new Dictionary<int, User>();

    //        string query = @"
    //        SELECT u.*, r.*
    //        FROM [User] u JOIN [UserRole] ur on u.Id = ur.UserId
    //                      JOIN [Role] r on ur.RoleId = r.Id
    //        WHERE u.Email = @email;";
    //        using (var connection = new SqlConnection(_options.ConnectionString))
    //        {
    //            var users = await connection.QueryAsync<User, Role, User>(query, (u, r) =>
    //            {
    //                // this lambda is called for each record retrieved by Dapper
    //                // receiving a user and a role created by Dapper from the record
    //                // and it is expected to return a user.
    //                // We look if the user passed in is already in the dictionary 
    //                // and add the role received to the roles list of that user
    //                if (!result.ContainsKey(u.Id))
    //                    result.Add(u.Id, u);
    //                User working = result[u.Id];
    //                working.Roles.Add(r);
    //                return u;
    //            }, new { email });

    //            // Return the first element in the dictionary
    //            if (result.Values.Count > 0)
    //                return result.Values.First();
    //            else
    //                return null;
    //        }

    //        //using (var connection = new SqlConnection(_options.ConnectionString))
    //        //{
    //        //    await connection.OpenAsync(cancellationToken);
    //        //    var results = await connection.QueryMultipleAsync(@"SELECT * FROM [User] WHERE Email = @email; " +
    //        //    "SELECT Id, Name, NormalizedName FROM [Role] JOIN [UserRole] ON [Role].Id = UserRole.RoleId" +
    //        //    " WHERE [UserRole].UserId = 2",  // <-- NEED TO INSERT USER ID DYNAMICALLY HERE
    //        //    new
    //        //    {
    //        //        email
    //        //    });

    //        //    var user = await results.ReadSingleAsync<User>();
    //        //    var roles = await results.ReadAsync<Role>();

    //        //    foreach (var role in roles)
    //        //    {
    //        //        user.Roles.Add(role);
    //        //    }

    //        //    return user;
    //        //}
    //    }

    //    public async Task<User> GetUserByIdAsync(int id, CancellationToken cancellationToken)
    //    {
    //        cancellationToken.ThrowIfCancellationRequested();

    //        using (var connection = new SqlConnection(_options.ConnectionString))
    //        {
    //            await connection.OpenAsync(cancellationToken);
    //            return await connection.QuerySingleOrDefaultAsync<User>($@"SELECT * FROM [User]
    //                WHERE [Id] = @{nameof(id)}", new { id });
    //        }
    //    }

    //    public async Task<IEnumerable<User>> GetUsersAsync(CancellationToken cancellationToken)
    //    {
    //        cancellationToken.ThrowIfCancellationRequested();

    //        using (var connection = new SqlConnection(_options.ConnectionString))
    //        {
    //            await connection.OpenAsync(cancellationToken);
    //            return await connection.QueryAsync<User>($@"SELECT * FROM [User]");
    //        }
    //    }
    //}
}
