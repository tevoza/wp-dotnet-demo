using JokesWebApp.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using JokesWebApp.Plugins;

namespace JokesWebApp
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddMvc();
            services.AddWordPress(options =>
            {
                //MySQL database connection
                options.DbHost = "192.168.1.16";//I used a different machine, can use localhost
                options.DbPassword = "password";
                options.DbName = "wordpress";

                //explicitely set url, or wordpress gets confused.
                //for example starting with kestrel vs iis, change the port accordingly
                // "/content" is used to demonstrate wordpress running next to MVC application
                //can be changed to just launch wordpress
                options.HomeUrl = "https://localhost:5001/content";
                options.SiteUrl = "https://localhost:5001/content";

                //Plugins written or registered in C#
                options.PluginContainer.Add(new DashboardPlugin());
                options.PluginContainer.Add(new DemoWidgetPlugin());
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //reroutes wordpress to /content
            app.Map(new PathString("/content"), wordpressApp =>
            {
                wordpressApp.UseWordPress();
            });
            //if undesired, remove the above and uncomment below
            //app.UseWordPress();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }

    }
}
