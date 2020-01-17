using Autofac;
using Kneat.SW.Ioc.Modules.Common;
using Kneat.SW.Ioc.Modules.Infrastructure;

namespace Kneat.SW.Ioc
{
    public class ApplicationContextBuilder : IApplicationContextBuilder
    {
        public IContainer Build()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new AutoMapperModule());
            builder.RegisterModule(new MediatorModule());
            builder.RegisterModule(new GatewaysModule());
            builder.RegisterModule(new ConfigurationModule());
            builder.RegisterModule(new CommonInfrastructureModule());

            var context = builder.Build();

            return context;
        }
    }
}
