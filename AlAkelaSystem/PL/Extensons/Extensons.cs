using BLL.Interfaces.ManagerServices;
using BLL.Services;
using DAL.unitofwork;
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
            // Register Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            
            // Register Services Manager
            services.AddScoped<IServicesManager, ServicesManager>();
            
            return services;
        }
        // atuomapper
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }
    }
}
