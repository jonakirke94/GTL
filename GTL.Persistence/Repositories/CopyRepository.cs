using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using GTL.Application.Interfaces.Repositories;
using GTL.Domain.Entities;
using GTL.Persistence.Configurations;
using Microsoft.Extensions.Options;
using GTL.Application.Interfaces.UnitOfWork;
using System.Linq;

namespace GTL.Persistence.Repositories
{
    public class CopyRepository : ICopyRepository
    {
        protected readonly IGTLContext Context;

        public CopyRepository(IGTLContext context)
        {
            Context = context;
        }

        public Copy GetCopyByBarcode(string barcode)
        {
            var query = $@"SELECT * FROM Copy WHERE Barcode = @barcode";
            using (var cmd = Context.CreateCommand())
            {
                var param = new DynamicParameters();
                param.Add("@barcode", barcode);
                var copy = cmd.Connection.Query<Copy>(query, param, cmd.Transaction);

                //connection.ExecuteScalar<Copy>(query, new { barcode });
                return copy.FirstOrDefault();
            }
        }
    }
}
