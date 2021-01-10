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
using System.Net.Http;
using System.Net;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

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
                    Configuration.GetConnectionString("CmentarzConnectionTEST")));
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("CmentarzConnectionTEST")));

            services.AddIdentity<IdentityUser, IdentityRole>()
               .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddDefaultUI();
            //    services.AddDefaultIdentity<IdentityUser>();    

            services.AddControllers(config =>
            {
                // using ... .Mvc.Authorization and Authorization
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddControllers().AddNewtonsoftJson(s =>
            {   // needed to get PATCH working
                s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft
                .Json.ReferenceLoopHandling.Ignore)

                .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver
                                    = new DefaultContractResolver());

            // enable CORS
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options =>
                             options.AllowAnyOrigin()
                                    .AllowAnyMethod()
                                    .AllowAnyHeader());
            });

            // SQL repositories
            //  services.AddScoped<ICommandRepo, SqlCommanderRepo>();
            services.AddScoped<IDeadPeopleRepo, SqlDeadPeopleRepo>();
            services.AddScoped<ILodgingsRepo, SqlLodgingsRepo>();
            services.AddScoped<INewsRepo, SqlNewsRepo>();
            services.AddScoped<IObituaryRepo, SqlObituaryRepo>();
            // DTOs
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // check page https://kenhaggerty.com/articles/article/aspnet-core-31-admin-role

            // Configuring Identity services
            services.Configure<IdentityOptions>(options =>
            {
                // !!!!!!!!!!!!!!!!!
                // CHECK IF WE NEED THIS, DELETE IF NEEDED
                // CONFIGURE LATER ON WHEN USERS ARE READY
                // !!!!!!!!!!!!!!!!!

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

            services.AddAuthorization(options =>
            {
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();

                options.AddPolicy("RequireAdministratorRole",
                    policy => policy.RequireRole("Administrator"));
                options.AddPolicy("RequireEmployeeRole",
                    policy => policy.RequireRole("Employee"));
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

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
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

            app.UseCors(options => options.WithOrigins("https://localhost:44357/api").AllowAnyMethod().AllowAnyHeader());
            app.UseRouting();


            app.UseAuthentication();
            //  app.UseIdentityServer();


            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute( // configure later for our main page
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}" //,
                                                                 //   defaults: new {controller = "Home", action = "Index"}
                    );
                endpoints.MapControllerRoute(
                    name: "api",
                    pattern: "api/{controller}/{id?}"
                    );

                endpoints.MapGet("/Identity/Account/Register", context => Task.Factory.StartNew(
                    () => context.Response.Redirect("/", true, true)));
                endpoints.MapPost("/Identity/Account/Register", context => Task.Factory.StartNew(
                    () => context.Response.Redirect("/", true, true)));
                endpoints.MapGet("/Identity/Account/Login", context => Task.Factory.StartNew(
                    () => context.Response.Redirect("/", true, true)));
                endpoints.MapPost("/Identity/Account/Login", context => Task.Factory.StartNew(
                    () => context.Response.Redirect("/", true, true)));
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

         //   serviceProvider = app.ApplicationServices.GetService<IServiceProvider>();
         //   CreateRoles(serviceProvider).Wait();
        //    ApplicationDbContext.CreateAdminAccount(serviceProvider, Configuration).Wait();
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            string[] roleNames = { "Administrator", "Employee", "BlockedEmployee" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    // if role doesnt exist, create one
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            // maintain the app by power user
            var powerUser = new IdentityUser
            {
                UserName = Configuration["AppSettings:UserName"],
                Email = Configuration["AppSettings:UserEmail"],
            };

            string userPWD = Configuration["AppSettings:UserPassword"];
            var _user = await UserManager.FindByEmailAsync(Configuration["AppSettings:AdminUserEmail"]);

            if(_user == null)
            {
                var createPowerUser = await UserManager.CreateAsync(powerUser, userPWD);
                if(createPowerUser.Succeeded)
                {
                    await UserManager.AddToRoleAsync(powerUser, "Administrator");
                }
            }
        }
    }
}
