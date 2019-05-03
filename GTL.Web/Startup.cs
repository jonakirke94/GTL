using GTL.Application.Interfaces.UnitOfWork;
using GTL.Persistence;
using GTL.Persistence.Configurations;
using GTL.Persistence.UnitOfWork;
using GTL.Web.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GTL.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<DataBaseSettings>(mySettings =>
            {
                mySettings.ConnectionString = Configuration.GetConnectionString("DefaultConnection");
            });

            services.AddMemoryCache();

            ServiceConfiguration.ConfigureServices(services);

            services.AddScoped<AuthExceptionFilter>();

            services.AddScoped<IGTLContext, GTLContext>();
            services.AddScoped<IConnectionFactory, ConnectionFactory>();

            PolicyConfiguration.ConfigurePolicies(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();


            app.UseMvcWithDefaultRoute();
        }
    }
}
