using Lib;
using Lib.Exception;
using NUnit.Framework;
using Tests.Entity;

namespace Tests
{
    public class Tests
    {
        private DependencyProvider _provider;

        [SetUp]
        public void Setup()
        {
            DependenciesConfiguration config = new DependenciesConfiguration();
            config.Register<IJustInterface, JustClass>();
            config.Register<IAnotherInterface, AnotherClass>();
            _provider = new DependencyProvider(config);
        }

        [Test]
        public void Test1()
        {
            Assert.Catch<CyclingDependencyException>(() =>  _provider.Resolve<IJustInterface>());
        }
    }
}