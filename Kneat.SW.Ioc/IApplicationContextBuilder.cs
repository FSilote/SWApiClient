using Autofac;

namespace Kneat.SW.Ioc
{
    public interface IApplicationContextBuilder
    {
        IContainer Build();
    }
}
