using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using GTL.Application.Interfaces.Repositories;
using GTL.Application.Interfaces.UnitOfWork;
using GTL.Domain.Entities;
using GTL.Persistence.Configurations;
using Microsoft.Extensions.Options;

namespace GTL.Persistence.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        protected readonly IGTLContext Context;

        public AddressRepository(IGTLContext context)
        {
            Context = context;
        }


        public void Add(Address address)
        {
            const string procedureName = "ADD_ADDRESS_WITH_USER";
            using (var cmd = Context.CreateCommand())
            {
                var para = new DynamicParameters();
                para.Add("@memberssn", address.MemberSsn);
                para.Add("@streetname", address.StreetName);
                para.Add("@housenumber", address.HouseNumber);
                para.Add("@zipcode", address.ZipCode);
                para.Add("@type", address.AddressType.ToString());
                para.Add("@city", address.City);

                cmd.Connection.Execute(procedureName, para, commandType: CommandType.StoredProcedure, transaction: cmd.Transaction);
            }
        }
    }
}
