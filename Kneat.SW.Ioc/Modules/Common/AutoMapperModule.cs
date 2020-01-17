using Autofac;
using AutoMapper;
using Kneat.SW.Domain.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Kneat.SW.Ioc.Modules.Common
{
    public class AutoMapperModule : Autofac.Module
    {
        private IList<dynamic> GetMaps()
        {
            var maps = (from t in Assembly.GetAssembly(typeof(BaseEntity)).GetTypes()
                        where t.IsClass
                           && !t.IsAbstract
                           && !t.IsInterface
                           && t.BaseType == typeof(Profile)
                           && t.GetConstructor(Type.EmptyTypes) != null
                        select Activator.CreateInstance(t))
                        .ToList();

            return maps;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var configuration = new MapperConfiguration(cfg => 
            {
                foreach(var profile in GetMaps())
                    cfg.AddProfile(profile);
            });

            builder.RegisterInstance<IMapper>(configuration.CreateMapper())
                .SingleInstance();
        }
    }
}
