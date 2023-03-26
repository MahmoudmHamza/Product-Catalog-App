using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using ProductsCatalogApp.Repositories;
using ProductsCatalogApp.Services;

namespace ProductsCatalogApp
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Services configuration
            services.AddScoped(typeof(IPostgresRepository<>), typeof(PostgresRepository<>));
            services.AddTransient<IProductService, ProductService>();
            
            // Database configuration
            var builder = new NpgsqlConnectionStringBuilder()
            {
                Database = Configuration["DBConfiguration:Database"],
                Host = Configuration["DBConfiguration:Server"],
                Port = Int32.Parse(Configuration["DBConfiguration:Port"]),
                Username = Configuration["DBConfiguration:Username"],
                Password = Configuration["DBConfiguration:Password"]
            };

            services.AddDbContext<CatalogDbContext>(options =>
            {
                options.UseNpgsql(builder.ConnectionString);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            try
            {
                Console.WriteLine("Migration Started");
                using (var service = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope())
                {
                    if (service != null)
                    {
                        var context = service.ServiceProvider.GetRequiredService<CatalogDbContext>();
                        context.Database.Migrate();
                    }
                }

                Console.WriteLine("Migration Done");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}