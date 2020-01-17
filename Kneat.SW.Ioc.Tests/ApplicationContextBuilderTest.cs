using Autofac;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kneat.SW.Ioc.Tests
{
    [TestClass]
    public class ApplicationContextBuilderTest
    {
        [TestMethod]
        public void GetApplicationContextBuilder_ShouldBeIContainer()
        {
            var container = new ApplicationContextBuilder().Build();
            Assert.IsInstanceOfType(container, typeof(IContainer));
        }

        [TestMethod]
        public void GetApplicationContextBuilder_ShouldBeNotNull()
        {
            var container = new ApplicationContextBuilder().Build();
            Assert.IsNotNull(container);
        }

        [TestMethod]
        public void GetMediator_ShouldBeNotNull()
        {
            var container = new ApplicationContextBuilder().Build();
            var mediator = container.Resolve<IMediator>();

            Assert.IsNotNull(mediator);
        }
    }
}
