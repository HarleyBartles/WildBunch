using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Cryptography;
using System.Text;
using WildBunch.Business.Entities;
using WildBunch.Business.Repositories;
using WildBunch.Business.Services;
using WildBunch.Data.DbContexts;
using WildBunch.Data.Repositories;
using WildBunch.Extensions;

namespace WildBunch
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
            var connectionString = Configuration.GetConnectionString("WildBunchContextConnection");

            services.AddControllersWithViews();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
            
            services.AddIdentityServices(connectionString);

            services.AddDataContextFactory<WildBunchContext>(connectionString);

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IGameRepository, GameRepository>();
            services.AddTransient<ICharacterRepository, CharacterRepository>();
            services.AddTransient<IInventoryObjectRepository, InventoryObjectRepository>();

            services.AddTransient<IGameService, GameService>();
            services.AddTransient<ICharacterService, CharacterService>();
            services.AddTransient<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseAuthentication();
            //app.UseIdentityServer();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var factory = serviceScope.ServiceProvider.GetRequiredService<DataContextFactory<WildBunchContext>>();
                
                using (var context = factory.Create())
                {
                    context.Database.EnsureCreated();
                }
            }
        }
    }

    public static class ServiceExtensions
    {
        public static void AddIdentityServices(this IServiceCollection services, string connectionString)
        {
            
            var serverVersion = ServerVersion.AutoDetect(connectionString);

            services.AddDbContext<WildBunchContext>(dbContextOptions =>
                    dbContextOptions
                    .UseMySql(connectionString, serverVersion)                    
                    .EnableSensitiveDataLogging() // <-- These two calls are optional but help
                    .EnableDetailedErrors()       // <-- with debugging (remove for production).
                );

            services.AddDefaultIdentity<WildBunchUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<WildBunchContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            });

            //services.AddIdentityServer()
            //    .AddApiAuthorization<WildBunchUser, WildBunchContext>();

            //services.AddAuthentication()
            //    .AddIdentityServerJwt();

            byte[] SecretKey;

            using (SHA256 sha256Hash = SHA256.Create())
            {
                SecretKey = sha256Hash.ComputeHash(Encoding.ASCII.GetBytes("WildBunchSecretKey"));
            };
                
            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddIdentityServerJwt()
                .AddJwtBearer(token =>
            {
                token.RequireHttpsMetadata = false;
                token.SaveToken = true;
                token.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    //Same Secret key will be used while creating the token
                    IssuerSigningKey = new SymmetricSecurityKey(SecretKey),
                    ValidateIssuer = true,
                    //Usually, this is your application base URL
                    ValidIssuer = "http://localhost:44392/",
                    ValidateAudience = true,
                    //Here, we are creating and using JWT within the same application.
                    //In this case, base URL is fine.
                    //If the JWT is created using a web service, then this would be the consumer URL.
                    ValidAudience = "http://localhost:44392/",
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddTransient<IWildBunchTokenProvider, WildBunchTokenProvider>();
        }
    }
}
