using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;*/

//using WebMarkupMin.AspNetCore2;

using SlickCMS.Data;
using Microsoft.EntityFrameworkCore;
using SlickCMS.Data.Services;
using SlickCMS.Data.Entities;
using SlickCMS.Data.Interfaces;

namespace SlickCMS.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        //public Startup()
        //public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
            this.HostingEnvironment = env;
        }

        // TODO: https://docs.microsoft.com/en-us/aspnet/core/performance/response-compression?view=aspnetcore-2.2

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                //options.CheckConsentNeeded = context => true;
                //options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSession();

            var connectionString = new SlickCMS.Core.ConnectionString();
            services.AddDbContext<SlickCMSContext>(options => options.UseSqlServer(connectionString.Get(this.HostingEnvironment.ContentRootPath)));

            // add Entity Services
            services.AddScoped<IPostService, PostService>();
        }

        /*public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }*/

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // see https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-2.2#order
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else if (env.IsProduction())
            {
                app.UseExceptionHandler("/Home/Error");
                //app.UseHsts();
                //app.UseWebMarkupMin();
            }

            //app.UseResponseCompression();
            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseCookiePolicy();
            //app.UseAuthentication();
            app.UseSession();

            app.UseMvc(routes =>
            {
                // static routes
                routes.MapRoute(name: "Static_About", template: "about", defaults: new { controller = "Static", action = "About" });
                routes.MapRoute(name: "Static_Projects", template: "projects", defaults: new { controller = "Static", action = "Projects" });
                routes.MapRoute(name: "Static_Contact", template: "contact", defaults: new { controller = "Static", action = "Contact" });
                routes.MapRoute(name: "Static_ProcessContact", template: "process-contact", defaults: new { controller = "Static", action = "ProcessContact" });
                routes.MapRoute(name: "Static_License", template: "license", defaults: new { controller = "Static", action = "License" });
                routes.MapRoute(name: "Static_Privacy", template: "privacy", defaults: new { controller = "Static", action = "Privacy" });
                routes.MapRoute(name: "Static_Cookies", template: "cookies", defaults: new { controller = "Static", action = "Cookies" });

                // TODO: route(s) for /posts and pagination
                // TODO: route(s) for individual Post

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        /*public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }*/
    }
}
