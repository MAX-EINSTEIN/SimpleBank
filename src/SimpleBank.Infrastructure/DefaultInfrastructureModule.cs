using Autofac;
using SimpleBank.Domain.Contracts;
using SimpleBank.Domain.Models;
using System.Reflection;

namespace SimpleBank.Infrastructure
{
    public class DefaultInfrastructureModule : Autofac.Module
    {
        private readonly List<Assembly> _assemblies = new List<Assembly>();

        public DefaultInfrastructureModule(Assembly? callingAssembly = null)
        {
            var coreAssembly =
              Assembly.GetAssembly(typeof(Address)); // TODO: Replace "Project" with any type from your Core project
            var infrastructureAssembly = Assembly.GetAssembly(typeof(StartupSetup));
            if (coreAssembly != null)
            {
                _assemblies.Add(coreAssembly);
            }

            if (infrastructureAssembly != null)
            {
                _assemblies.Add(infrastructureAssembly);
            }

            if (callingAssembly != null)
            {
                _assemblies.Add(callingAssembly);
            }
        }

        protected override void Load(ContainerBuilder builder)
        {
           
            builder.RegisterType<BankRepository>()
                .As(typeof(IBankRepository))
                .As(typeof(IRepository<Bank>))
                .InstancePerLifetimeScope();
        }
    }
}
