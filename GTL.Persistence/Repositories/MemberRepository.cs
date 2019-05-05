using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using GTL.Application.Interfaces.Repositories;
using GTL.Application.Interfaces.UnitOfWork;
using GTL.Domain.Entities;
using GTL.Persistence.Configurations;
using Microsoft.Extensions.Options;

namespace GTL.Persistence.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        protected readonly IGTLContext Context;

        public MemberRepository(IGTLContext context)
        {
            Context = context;
        }

        public void Add(Member member)
        {
            const string query = @"INSERT INTO [Member] ([Ssn], [Name], [Email], [Type]) VALUES(@ssn, @name, @email, @type)";
            using(var cmd = Context.CreateCommand())
            {
                var para = new DynamicParameters();
                para.Add("@ssn", member.Ssn);
                para.Add("@name", member.Name);
                para.Add("@email", member.Email);
                para.Add("@type", member.Type.ToString());

                cmd.Connection.Execute(query, para, cmd.Transaction);
            }
        }

        public Member GetBySsn(string ssn)
        {
            const string query = @"SELECT * FROM Member WHERE ssn = @ssn";
            using (var cmd = Context.CreateCommand())
            {
                var para = new DynamicParameters();
                para.Add("@ssn", ssn);
                var results = cmd.Connection.Query<Member>(query, para, cmd.Transaction);
                return results.FirstOrDefault();
            }
        }
    }
}
