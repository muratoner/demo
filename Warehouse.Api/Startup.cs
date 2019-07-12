using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using Warehouse.Api.Helper;
using Warehouse.Api.Validation;
using Warehouse.Business.Services;
using Warehouse.Data;
using Warehouse.Data.Entities;

namespace Warehouse.Api
{
    public class Startup
    {
        private const string AllowedOrigins = "_allowedOrigins";

        private readonly IHostingEnvironment _env;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(AllowedOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost:50050")
                                        .AllowAnyHeader()
                                        .AllowAnyMethod();
                });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Warehouse - Product Manager - Api",
                    Description = "You can manage products for companies warehouse."
                });
            });

            services.AddTransient<IServiceBase<Product>, ServiceProduct>();
            services.AddTransient<IValidator<Product>, ValidationProduct>();

            services.AddDbContext<WarehouseDbContext>(opt =>
            {
                opt.UseSqlite($"Data Source={_env.ContentRootPath}\\Warehouse.db");
            });

            services.AddHttpContextAccessor();
            
            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.Formatting = Formatting.Indented;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Fill database table with faker data.
            HelperDbInitializer.Seed(app);

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseCors(AllowedOrigins);

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseMvc();
        }
    }
}
