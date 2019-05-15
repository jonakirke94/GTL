using System;
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
    public class CopyRepository : ICopyRepository
    {
        protected readonly IGTLContext _context;

        public CopyRepository(IGTLContext context)
        {
            _context = context;
        }

        public Copy GetByBarcode(int barcode)
        {
            const string query = @"SELECT * FROM Copy WHERE Barcode = @barcode";
            using (var cmd = _context.CreateCommand())
            {
                var para = new DynamicParameters();
                para.Add("@barcode", barcode);
                var results = cmd.Connection.Query<Copy>(query, para, cmd.Transaction);
                return results.FirstOrDefault();
            }
        }
    }
}
