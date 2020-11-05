using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace BethanysPieShop
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Models.DataBaseContext>(option =>
            option.UseSqlServer(configuration.GetConnectionString("DataBaseContext")));

            services.AddScoped<DataAccessLayer.IUnitOfWork,DataAccessLayer.UnitOfWork>();
            
            services.AddHttpContextAccessor();

            services.AddIdentity<Microsoft.AspNetCore.Identity.IdentityUser, Microsoft.AspNetCore.Identity.IdentityRole>(options=>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredUniqueChars = 0;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                //User settings.
                options.User.RequireUniqueEmail = true;
            })
              .AddEntityFrameworkStores<Models.DataBaseContext>()
              .AddDefaultTokenProviders();

            //services.TryAddScoped<UserManager<Models.User>>();
            //services.TryAddScoped<SignInManager<Models.User>>();
            //services.TryAddScoped<RoleManager<Models.Role>>();

            services.AddControllersWithViews();
            services.AddSession();
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,RoleManager<IdentityRole> roleManager,UserManager<IdentityUser> userManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseSession();//order important
            app.UseRouting();
            //app.UseMvc();

            app.UseAuthentication();
            app.UseAuthorization();
            Infrastracture.DataInitilizer.SeedData(roleManager, userManager);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Administration",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
