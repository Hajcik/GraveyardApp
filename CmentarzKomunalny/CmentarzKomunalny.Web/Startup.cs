using AutoMapper;
using CmentarzKomunalny.Web.Data;
using CmentarzKomunalny.Web.Data.Contexts;
using CmentarzKomunalny.Web.Data.Interfaces;
using CmentarzKomunalny.Web.Data.Repositories;
using CmentarzKomunalny.Web.Models;
using CmentarzKomunalny.Web.Models.Cmentarz;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using System;

namespace CmentarzKomunalny.Web
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
            // we're adding context over here !!!!!!!!!!
            // switch it up for CmentarzContext when it will be ready to go
            services.AddDbContext<CmentarzContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("CmentarzConnection")));

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(); 
                // if something doesnt work change back to "ApplicationUser, ApplicationDbContext"
                // and check why it doesnt work


            services.AddControllers().AddNewtonsoftJson(s => 
            {   // needed to get PATCH working
                s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            //        services.AddScoped<ICommandRepo, MockCommanderRepo>();
            services.AddScoped<ICommandRepo, SqlCommanderRepo>();

            // DTOs
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddIdentity<User, Role>()
                .AddRoles<Role>()
                .AddRoleManager<RoleManager<Role>>()
                .AddEntityFrameworkStores<UserContext>() // basing on UserContext it creates identity
                .AddSignInManager<SignInManager<User>>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();
            // Authorization (admin, employee etc...)
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admins", policy =>
                {
                    policy.RequireRole("Admin");
                });

                options.AddPolicy("Employees", policy =>
                {
                    policy.RequireRole("Employee");
                });
            });

            services.AddRazorPages(options =>
            {   // later edit pages that needs admin auth to be used by admin
                // like Index.cshtml and others, make folders like /Admin and stuff
                options.Conventions.AuthorizeFolder("/Admin");
                options.Conventions.AuthorizeFolder("/Admin/Users", "Admins");
            });
            // check page https://kenhaggerty.com/articles/article/aspnet-core-31-admin-role

            // Configuring Identity services
            services.Configure<IdentityOptions>(options =>
            {
                // password settings
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;

                // lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(4); // 4 minutes to logout
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.AllowedForNewUsers = false; // check it later, only admin can create new user, you can't register by yourself

                // user settings
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true; // check it later
              
            });
            
            services.AddAuthentication()
                .AddIdentityServerJwt();
            services.AddControllersWithViews();
            services.AddRazorPages();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
