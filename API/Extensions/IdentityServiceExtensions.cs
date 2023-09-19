using Infrastracture.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppIdentityDbContext>(
                opt => opt.UseSqlServer(configuration.GetConnectionString(("IdentityConnection"))));
            return services;
        }
    }
}
