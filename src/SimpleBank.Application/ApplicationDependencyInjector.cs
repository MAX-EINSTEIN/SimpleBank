using Microsoft.Extensions.DependencyInjection;
using SimpleBank.Application.Contracts;
using SimpleBank.Application.Services;

namespace SimpleBank.Application
{
    public static class ApplicationDependencyInjector
    {
        public static void AddApplicatonServices(this IServiceCollection services)
        {
            services.AddScoped<IFundTransferService, FundTransferService>();
        }
    }
}