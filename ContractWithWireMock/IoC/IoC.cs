using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Integration.WebApi;
using ContractWithWireMock.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ContractWithWireMock.IoC
{
    public class IoC
    {
        public static IContainer Container { get; private set; } = new ContainerBuilder().Build();

        public static IContainer GetAutofaContainer(IServiceCollection services)
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterAssemblyTypes(typeof(DecisionService).Assembly).AsImplementedInterfaces();

            builder.Populate(services);

            return Container = builder.Build();
        }
    }
}
