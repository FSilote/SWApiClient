using Autofac;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Kneat.SW.Ioc.Modules.Common
{
    public class ConfigurationModule : Autofac.Module
    {
        private static IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance<IConfiguration>(GetConfiguration())
                .SingleInstance();
        }
    }
}
