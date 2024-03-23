using BookStore.Application.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IAppDbContext, AppDBContext>(options =>
                options
                    .UseLazyLoadingProxies()
                    .UseNpgsql(configuration.GetConnectionString("Postgres")));

            return services;
        }
    }
}
