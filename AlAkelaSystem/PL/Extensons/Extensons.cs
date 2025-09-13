using DAL.Repository;
using Microsoft.EntityFrameworkCore;

namespace PL.Extensons
{
    public static class Extensons
    {
        public static IServiceCollection AddApplactionBDContext(this IServiceCollection services,IConfiguration configuration)
        {

            services.AddDbContext<DAL.Data.AlAkelaDBcontext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            return services;
        }

        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<DAL.unitofwork.UnitOfWork>();
            return services;
        }
    }
}
