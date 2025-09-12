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
    }
}
