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
    public class MemberRepository : IMemberRepository
    {
        private DataBaseSettings Options { get; }

        public MemberRepository(IOptions<DataBaseSettings> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public Member GetMemberBySsn(string ssn)
        {

            var query = $@"SELECT * FROM Members WHERE Ssn = @ssn";
            using (var connection = new SqlConnection(Options.ConnectionString))
            {
                connection.Open();
                var member = connection.ExecuteScalar<Member>(query, new { ssn });
                return member;
            }

        }
    }
}
