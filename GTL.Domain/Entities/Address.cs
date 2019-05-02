using System;
using System.Collections.Generic;
using System.Text;
using GTL.Domain.Enums;

namespace GTL.Domain.Entities
{
    public class Address
    {
        public int Id { get; set; }

        public string StreetName { get; set; }

        public string HouseNumber { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }

        public AddressType AddressType { get; set; }

        public string MemberSsn { get; set; }
    }
}
