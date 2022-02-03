using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ShoppingListApi.Services;

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
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShoppingListApi", Version = "v1" });
            });

            // Add dependency of IItemGenerator
            // add.Transient- gets created for every time reference
            // add.Scoped- gets created per request
            // add.Singleton- created one
            // Add dependency of IShoppingListService
            services.AddSingleton<IShoppingListService, ShoppingListService>();

            // Add dependency of IItemsGenerator
            services.AddSingleton<IItemsGenerator, ItemsGenerator>();

            services.AddSingleton<ITaxedShoppingListConverter, TaxedShoppingListConverter>();

            // Add dependency of ITaxPolicy
            services.AddSingleton<ITaxPolicy, ProgressiveTaxedPolicy>();

            // way to custom create a thing, not only inject it
            services.AddSingleton<ITaxPolicy, FixedTaxPolicy>( _ => new FixedTaxPolicy(1.01m));
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
