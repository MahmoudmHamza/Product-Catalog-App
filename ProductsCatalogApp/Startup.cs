using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Npgsql;
using ProductsCatalogApp.Repositories;
using Newtonsoft.Json;
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
            // Add Swagger documentation
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Simple Online Store API", Version = "v1" });
            });
            
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            
            // Services configuration
            services.AddScoped(typeof(IPostgresRepository<>), typeof(PostgresRepository<>))
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IProductRepository, ProductRepository>()
                .AddScoped<IOrderRepository, OrderRepository>()
                .AddScoped<IProductService, ProductService>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<ICategoryService, CategoryService>()
                .AddScoped<IOrderService, OrderService>();

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
                
                // Enable Swagger UI
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Simple Online Store API V1"));
            }

            try
            {
                Console.WriteLine("Migration Started...");
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