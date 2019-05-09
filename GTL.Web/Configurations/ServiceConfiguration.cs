using AutoMapper;
using FluentValidation.AspNetCore;
using GTL.Application.Interfaces.Repositories;
using GTL.Web.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using GTL.Application;
using GTL.Application.Features.Loans.Commands.CreateLoan;
using GTL.Application.Infrastructure.AutoMapper;
using GTL.Application.Infrastructure.Pipeline;
using GTL.Application.Interfaces;
using GTL.Application.Interfaces.Authentication;
using GTL.Application.Interfaces.UnitOfWork;
using GTL.Persistence;
using GTL.Persistence.Repositories;
using GTL.Persistence.UnitOfWork;
using GTL.Persistence.Configurations;

namespace GTL.Web.Configurations
{
    public static class ServiceConfiguration
    {
        public static void ConfigureServices(IServiceCollection services)
        {         
            services.AddMemoryCache();

            // repos
            services.AddScoped<ILoanRepository, LoanRepository>();
            services.AddScoped<IStaffRepository, StaffRepository>();


            // services related to authentication and authorization
            services.AddScoped<ISignInManager, SignInManager>();
            services.AddScoped<IPasswordHelper, PasswordHelper>();
            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddHttpContextAccessor();

            services.AddScoped<CustomCookieAuthenticationEvents>();

            // Add AutoMapper
            services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly });

            // Add MediatR
            services.AddMediatR(typeof(CreateLoanCommand).GetTypeInfo().Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestAuthBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddScoped<AuthExceptionFilter>();

            services.AddScoped<IGTLContext, GTLContext>();
            services.AddScoped<IConnectionFactory, ConnectionFactory>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
                AddCookie((options) =>
                {
                    options.EventsType = typeof(CustomCookieAuthenticationEvents);
                });

            services.AddRouting(options => options.LowercaseUrls = true);
            
            services
              .AddMvc(config => {
                  config.Filters.Add(new AuthExceptionFilter());
              })
              .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
              .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateLoanCommandValidator>());
        }
    }
}
