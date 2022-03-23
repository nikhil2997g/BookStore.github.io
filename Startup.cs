using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
using Microsoft.EntityFrameworkCore;
using BookStore.Repository;
using Microsoft.AspNetCore.Identity;
using BookStore.Models;
using BookStore.Helpers;

namespace BookStore
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
            services.AddControllersWithViews();
#if DEBUG
            //by adding above condition we can use reloading views only during Development
            services.AddRazorPages().AddRazorRuntimeCompilation();
              
            //uncomment this code to disable client side validations
            //    .AddViewOptions(options => {
            //    options.HtmlHelperOptions.ClientValidationEnabled = false;
            //});
#endif
            services.AddDbContext<BookStoreContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //adding configuration services for identity along with the dbcontext on which the identity should work
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<BookStoreContext>();

            //we can configure our own password requirement by using below service
            //services.Configure<IdentityOptions>(options =>
            //{
            //    options.Password.RequiredLength = 5;
            //    options.Password.RequireDigit = true;

            //});

            //in order to add new book the page is redirected to login with below service
            services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Login";
            });
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            //for adding claims in order have our firstname or lastname or profilelink we use below service along with ApplicationUserClaimsPrincipalFactory class from helpers folder
            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimsPrincipalFactory>();
            
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
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
