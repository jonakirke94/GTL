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
    public class AddressRepository : IAddressRepository
    {

        private DataBaseSettings Options { get; }

        public AddressRepository(IOptions<DataBaseSettings> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }


        public void AddAddress(Address address)
        {

        using (var connection = new SqlConnection(Options.ConnectionString))
            {
                connection.Open();
                connection.Execute($@"INSERT INTO [Address] ([StreetName], [HouseNumber], [ZipCode], [City], [Type], [MemberSsn])
                    VALUES (@{nameof(address.StreetName)}, @{nameof(address.HouseNumber)}, @{nameof(address.ZipCode)}, @{nameof(address.City)}, @{nameof(address.AddressType)}, @{nameof(address.MemberSsn)});",
                    address);
            }
        }
    }
}
