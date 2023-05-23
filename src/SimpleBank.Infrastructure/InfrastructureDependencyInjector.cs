using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleBank.Domain.BankAccountAggregate;
using SimpleBank.Domain.BankAggregate;
using SimpleBank.Domain.BankBranchAggregate;
using SimpleBank.Domain.FundTransferAggregate;
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
