using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ShoppingListApi.Bootstrap;
using ShoppingListApi.Db;
using ShoppingListApi.Services;
using ShoppingListApi.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ShoppingListApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // Register dependencies
        // IServicesCollection is an Inversion of Control containers
        // It inverts the place when the objects get created
        // They get created (configured) here
        public void ConfigureServices(IServiceCollection services)
        {
            // Composition root- a place where you register all the dependencies.

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShoppingListApi", Version = "v1" });
            });

            // Add dependency of IItemGenerator
            // add.Transient- gets created for every time reference
            // add.Scoped- gets created per request
            // add.Singleton- created one
            // Add dependency of IShoppingListService,
            // change to AddTransient from AddSingleton because we dont need to do this statically after implementing DB
            services.AddTransient<IShoppingListService, ShoppingListService>();

            // Add dependency of IItemsGenerator
            services.AddTransient<IItemsGenerator, ItemsGenerator>();

            services.AddTransient<IShoppingListRepository, ShoppingListRepository>();
            services.AddTransient<IItemsRepository, ItemsRepository>();

            // Setup class through extension method
            services.AddTaxPolicies();

            // Hook up db context within app
            services.AddDbContext<ShoppingContext>( builder =>
            {
                builder.UseSqlite(@"DataSource=ShoppingList.db;");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // Configure middleware
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShoppingListApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
