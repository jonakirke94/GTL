﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using GTL.Application.Interfaces.Repositories;
using GTL.Application.Interfaces.UnitOfWork;
using GTL.Domain.Entities;
using GTL.Persistence.Configurations;

namespace GTL.Persistence.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        protected readonly IGTLContext _context;

        public MemberRepository(IGTLContext context)
        {
            _context = context;
        }
        public Member GetByLoanerCard(int barcode)
        {
            const string query = @"SELECT m.* FROM Member m LEFT JOIN LoanerCard l ON m.Ssn = l.MemberSsn WHERE l.Barcode = @barcode";
            using (var cmd = _context.CreateCommand())
            {
                var para = new DynamicParameters();
                para.Add("@barcode", barcode);
                var results = cmd.Connection.Query<Member>(query, para, cmd.Transaction);
                return results.FirstOrDefault();
            }
        }
    }
}
