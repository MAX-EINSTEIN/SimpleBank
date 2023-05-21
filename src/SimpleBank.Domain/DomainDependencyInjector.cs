using Microsoft.Extensions.DependencyInjection;
using SimpleBank.Domain.Contracts;
using SimpleBank.Domain.Services;

namespace SimpleBank.Domain
{
    public static class DomainDependencyInjector
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddTransient<IBankBranchManagementService, BankBranchManagementService>();

            return services;
        }
    }
}
