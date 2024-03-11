using Microsoft.EntityFrameworkCore;
using FinancyControl.API.Controllers.Config;
using FinancyControl.API.Domain.Repositories;
using FinancyControl.API.Domain.Services;
using FinancyControl.API.Extension;
using FinancyControl.API.Persistence.Contexts;
using FinancyControl.API.Persistence.Repositories;
using FinancyControl.API.Services;

namespace FinancyControl.API
{
    public class Startup
    {
        private readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();

            services.AddCustomSwagger();

            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

            services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                // Adds a custom error response factory when ModelState is invalid
                options.InvalidModelStateResponseFactory = InvalidModelStateResponseFactory.ProduceErrorResponse;
            });

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase(Configuration.GetConnectionString("memory") ?? "data-in-memory");
            });

            services.AddScoped<ITipoDespesaRepository, TipoDespesaRepository>();
            services.AddScoped<IDespesaRepository, DespesaRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ITipoDespesaService, TipoDespesaService>();
            services.AddScoped<IDespesaService, DespesaService>();

            services.AddAutoMapper(typeof(Startup));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCustomSwagger();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
