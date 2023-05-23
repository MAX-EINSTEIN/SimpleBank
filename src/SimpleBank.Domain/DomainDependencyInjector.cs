using Microsoft.Extensions.DependencyInjection;
using SimpleBank.Domain.BankAccountAggregate;
using SimpleBank.Domain.BankBranchAggregate;

namespace SimpleBank.Domain
{
    public static class DomainDependencyInjector
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddTransient<IBankBranchManagementService, BankBranchManagementService>();
            services.AddTransient<IBankAccountManagementDomainService, BankAccountManagementDomainService>();
            return services;
        }
    }
}
