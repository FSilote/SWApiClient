using Autofac;
using Kneat.SW.Domain.Infrastructure.Common;
using Kneat.SW.Infrastructure.Gateways.Common;

namespace Kneat.SW.Ioc.Modules.Infrastructure
{
    public class CommonInfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HttpGateway>()
                .As<IHttpGateway>()
                .InstancePerLifetimeScope();
        }
    }
}
