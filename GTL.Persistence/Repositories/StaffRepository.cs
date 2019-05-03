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

        public Staff GetBySsn(string ssn)
        {
            const string query = @"SELECT * FROM Staff WHERE ssn = @ssn";
            using (var cmd = Context.CreateCommand())
            {
                var para = new DynamicParameters();
                para.Add("@ssn", ssn);
                var results = cmd.Connection.Query<Staff>(query, para, cmd.Transaction);
                return results.FirstOrDefault();
            }
        }
    }
}
