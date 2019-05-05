using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using GTL.Application.Interfaces.Repositories;
using GTL.Application.Interfaces.UnitOfWork;
using GTL.Domain.Entities;

namespace GTL.Persistence.Repositories
{
    public class StaffRepository : IStaffRepository
    {
        private readonly IGTLContext Context;

        public StaffRepository(IGTLContext context)
        {
            Context = context;
        }

        public Staff GetById(int id)
        {
            const string query = @"SELECT * FROM Staff WHERE id = @id";
            using (var cmd = Context.CreateCommand())
            {
                var para = new DynamicParameters();
                para.Add("@id", id);
                var results = cmd.Connection.Query<Staff>(query, para, cmd.Transaction);
                return results.FirstOrDefault();
            }
        }

        public Staff GetByEmail(string email)
        {
            const string query = @"SELECT * FROM Staff WHERE email = @email";
            using (var cmd = Context.CreateCommand())
            {
                var para = new DynamicParameters();
                para.Add("@email", email);
                var results = cmd.Connection.Query<Staff>(query, para, cmd.Transaction);
                return results.FirstOrDefault();
            }
        }
    }
}
