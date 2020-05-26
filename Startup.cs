using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SodaMachine.Database;
using SodaMachine.Services;

namespace SodaMachine
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
            string inMemoryDbName = "SodaMachineInMemoryDb";

            //Using the in-memory database
            services.AddDbContext<SodaMachineDBContext>(opt => opt.UseInMemoryDatabase(inMemoryDbName));

            //Get the db context from the services
            var serviceProvide = services.BuildServiceProvider();
            var dbContext = serviceProvide.GetService<SodaMachineDBContext>();

            //Seed DB inside context
            DatabaseInitializer.Initialize(dbContext);

            //Pass DB context to the Controller-injectible DB service
            services.AddScoped<ISodaMachineDbService, SodaMachineDbService>(s => new SodaMachineDbService(dbContext));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            // In production, the Angular files will be served from this directory
            /*
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
            */
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            // app.UseSpaStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller}/{action=Index}/{id?}");
            });

            /*
            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                // If development - ng serve ClientApp yourself.
                // if (env.IsDevelopment())
                // {
                //    spa.UseAngularCliServer(npmScript: "start");
                // }
            });
            */

        }
    }
}
