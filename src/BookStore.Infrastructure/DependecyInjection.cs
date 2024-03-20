using BookStore.Application.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IAppDbContext, AppDBContext>();

            //services.AddIdentity<User, IdentityRole>()
            //    .AddEntityFrameworkStores<AppDBContext>()
            //    .AddDefaultTokenProviders();

            return services;
        }
    }
}
