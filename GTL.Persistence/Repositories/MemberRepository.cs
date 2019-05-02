using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using GTL.Application.Interfaces.Repositories;
using GTL.Domain.Entities;
using GTL.Persistence.Configurations;
using Microsoft.Extensions.Options;

namespace GTL.Persistence.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private DataBaseSettings Options { get; }

        public MemberRepository(IOptions<DataBaseSettings> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public void CreateMember(Member member)
        {
            using (var connection = new SqlConnection(Options.ConnectionString))
            {
                connection.Open();
                connection.Execute($@"INSERT INTO [Member] ([Ssn], [Name], [Email], [Type])
                    VALUES (@{nameof(member.Ssn)}, @{nameof(member.Name)}, @{nameof(member.Email)}, @{nameof(member.Type)});",
                    member);
            }
        }

        public Member GetMemberBySsn(string ssn)
        {
            var query = $@"SELECT * FROM Member WHERE ssn = @ssn";
            using (var connection = new SqlConnection(Options.ConnectionString))
            {
                connection.Open();
                var user = connection.Query<Member>(query, new { ssn });
                return user.FirstOrDefault();
            }
        }
    }
}
