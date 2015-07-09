using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Data.Entity;
using MovieAngular.Models;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MovieAngular
{
    public class Startup
    {
        public Startup()
        {
            Configuration = new Configuration()
                        .AddJsonFile("Config.json")
                        .AddEnvironmentVariables();
        }

        public IConfiguration Configuration { get; set; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // Add EF services to the service container
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<MoviesAppContext>(options =>
                {
                    options.UseSqlServer(Configuration.Get("Data:DefaultConnection:ConnectionString"));
                });

            // Add Identity services to the services container
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<MoviesAppContext>()
                    .AddDefaultTokenProviders();
        }

        public void Configure(IApplicationBuilder app)
        {

            // Add static files
            app.UseStaticFiles();

            app.UseMvc();

            SampleData.InitializeMoviesDatabaseAsync(app.ApplicationServices).Wait();
        }

    }
}
