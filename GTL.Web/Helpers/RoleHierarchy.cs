using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTL.Web.Helpers
{
    public static class RoleHierarchy
    {
        public const string CHIEFLIBRARIAN = "CHIEFLIBRARIAN";
        public const string ASSOCIATELIBRARIAN = "CHIEFLIBRARIAN, ASSOCIATELIBRARIAN";
        public const string REFERENCELIBRARIAN = "REFERENCELIBRARIAN, CHECKOUTSTAFF, LIBRARYASSISTANT";
        public const string CHECKOUTSTAFF = "CHIEFLIBRARIAN, ASSOCIATELIBRARIAN, REFERENCELIBRARIAN, CHECKOUTSTAFF";
        public const string LIBRARYASSISTANT = "CHIEFLIBRARIAN, ASSOCIATELIBRARIAN, REFERENCELIBRARIAN, CHECKOUTSTAFF, LIBRARYASSISTANT";
    }
}

