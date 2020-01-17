using Autofac;
using Kneat.SW.Domain.Infrastructure.Gateways;
using Kneat.SW.Infrastructure.Gateways.SWApi_co;

namespace Kneat.SW.Ioc.Modules.Infrastructure
{
    public class GatewaysModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SWApiGateway>()
                .As<ISWApiGateway>()
                .InstancePerLifetimeScope();
        }
    }
}
