using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Solution.Core.Interfaces.Comman;
using Solution.Core.Interfaces.Dapper;
using Solution.Infrastructure.Dapper;
using Solution.Infrastructure.Persistence;

namespace Solution.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EmployeeDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("ConnectionString"),
                    builder =>
                    {
                        builder.MigrationsAssembly(typeof(EmployeeDbContext).Assembly.FullName);
                    });
            });

            /*services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
            services.AddSingleton<IDapperContext, DapperContext>();*/
            services.AddScoped<IEmployeeDbContext>(provider =>
                provider.GetRequiredService<EmployeeDbContext>());
            services.AddSingleton<IDapperContext, DapperContext>();

            return services;
        }
    }
}
