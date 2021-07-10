using System;
using BoulderPOS.API.Middleware;
using BoulderPOS.API.Persistence;
using BoulderPOS.API.Services;
using BoulderPOS.API.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace BoulderPOS.API
{
    public class Startup
    {
        private const string CorsAllowedOrigins = "corsAllowedOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Cross Origin Resource Sharing
            services.AddCors(options =>
                {
                    options.AddPolicy(CorsAllowedOrigins, builder =>
                    {
                        var allowedOrigins = Configuration.GetSection("AllowedOrigins");
                        var origins = new []
                        {
                            allowedOrigins.GetValue<string>("ReverseProxyConnection"),
                            allowedOrigins.GetValue<string>("WaiverConnection")
                        };
                        builder.WithOrigins(origins)
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                });

                });
            
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseNpgsql(EnvironmentHelper.RunningInDocker
                    ? Configuration.GetConnectionString("DockerConnection")
                    : Configuration.GetConnectionString("LocalhostConnection"));
                options.UseCamelCaseNamingConvention();
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "Alpha",
                    Title = "BoulderPOS API"
                });
            });

            ConfigureIoC(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "BoulderAPI Alpha");
                });
            }
            
            app.UseCors(CorsAllowedOrigins);

            app.UseRouting();
            
            app.UseAuthorization();

            app.UseRequestResponseLogging();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureIoC(IServiceCollection services)
        {
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<ICustomerEntriesService, CustomerEntriesService>();
            services.AddTransient<ICustomerSubscriptionService, CustomerSubscriptionService>();
            services.AddTransient<IProductCategoryService, ProductCategoryService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductInventoryService, ProductInventoryService>();
            services.AddTransient<IBillProductService, BillProductService>();
            services.AddTransient<IBillService, BillService>();
        }
    }
}
