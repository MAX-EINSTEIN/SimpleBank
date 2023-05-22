using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleBank.Domain.Contracts;
using SimpleBank.Infrastructure.Repositories;

namespace SimpleBank.Infrastructure
{
    public static class InfrastructureDependencyInjector
    {
        public static void AddDbContext(this IServiceCollection services, string connectionString) =>
            services.AddDbContext<SimpleBankDbContext>(options =>
                options.UseSqlServer(connectionString)
            );

        public static void AddPersistence(this IServiceCollection services)
        {
            services.AddScoped<IBankRepository, BankRepository>();
            services.AddScoped<IFundTransferRepository, FundTransferRepository>();
            services.AddScoped<IBankBranchRepository, BankBranchRepository>();
            services.AddScoped<IBankAccountRepository, BankAccountRepository>();
        }
    }
}
