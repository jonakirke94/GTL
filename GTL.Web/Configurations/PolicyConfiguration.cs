using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GTL.Domain.Enums;
using GTL.Web.Helpers;

namespace GTL.Web.Configurations
{
    public static class PolicyConfiguration
    {
        public static void ConfigurePolicies(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    PolicyNames.CanReadUsers,
                    policy => policy.RequireRole(MinRole(Role.CHIEFLIBRARIAN)));

                options.AddPolicy(
                    PolicyNames.CanWriteUsers,
                    policy => policy.RequireRole(MinRole(Role.CHECKOUTSTAFF)));

                options.AddPolicy(
                    PolicyNames.CanCreateLoanerCard,
                    policy => policy.RequireRole(MinRole(Role.ASSOCIATELIBRARIAN)));
            });
        }

        public static string[] MinRole(Role role)
        {
            return Enum.GetValues(typeof(Role)).Cast<Role>().Where(r => role >= r).Select(x => x.ToString()).ToArray();
        }
    }
}
