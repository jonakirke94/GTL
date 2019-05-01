using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTL.Web.Configurations
{
    public static class PolicyConfiguration
    {
        public static void ConfigurePolicies(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    "CanReadUsers",
                    policy => policy.RequireRole("CHIEFLIBRARIAN"));

                options.AddPolicy(
                    "CanWriteUsers",
                    policy => policy.RequireRole("CHIEFLIBRARIAN"));

                options.AddPolicy(
                    "CanCreateLoanerCard",
                    policy => policy.RequireRole("CHIEFLIBRARIAN", "REFERENCELIBRARIAN"));
            });
        }
    }
}
