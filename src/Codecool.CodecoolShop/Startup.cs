using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.Core.Models;
using EFCoreInMemory;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EFDataAccessLibrary.DataAccess;

namespace Codecool.CodecoolShop
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
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            //services.AddDbContext<InMemoryDb>(options => options.UseInMemoryDatabase("CCShopInMemoryDb"));
            services.AddDbContext<CCShopContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));
            //services.AddDbContext<InMemoryDb>(options => options.UseInMemoryDatabase("InMemoryDb"));
            services.AddSession(options =>
            {
                options.Cookie.Name = ".CodeCoolShop.Session";
                options.Cookie.IsEssential = true;
            });
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
                app.UseExceptionHandler("/Product/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Product}/{action=Index}/{id?}");
            });

            // Generate In Memory Data: // TODO: how to refactor this? => move this to a separate service (call after app is started, not before)

            //1. Find the service layer within our scope.
            //using (var scope = app.ApplicationServices.CreateScope())
            //{
            //    //2. Get the instance of InMemoryDb in our services layer
            //    var services = scope.ServiceProvider;
            //    var context = services.GetRequiredService<InMemoryDb>();

            //    //3. Call the DataGenerator to create sample data
            //    InMemoryDataGenerator.Init(context); // InMemory Data Generation
            //}
        }
    }
}
