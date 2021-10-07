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
            config.Register<GenericArgument, GenericArgument>();
            config.Register<GenericClass<GenericArgument>, GenericClass<GenericArgument>>();
            _provider = new DependencyProvider(config);
        }

        [Test]
        public void CyclingDependencyTest()
        {
            Assert.Catch<CyclingDependencyException>(() =>  _provider.Resolve<IJustInterface>());
        }

        [Test]
        public void GenericTest()
        {
            GenericClass<GenericArgument> genericClass = _provider.Resolve<GenericClass<GenericArgument>>();
            Assert.NotNull(genericClass);
            Assert.NotNull(genericClass.Something);
        }

        [Test]
        public void DependenciesConfigurationExceptionTest()
        {
            Assert.Catch<CannotBeValueTypeException>(() => 
                new DependenciesConfiguration().Register<int, int>());
            Assert.Catch<ImplementationCannotBeAbstractException>(() =>
                new DependenciesConfiguration().Register<IJustInterface, AbstractClass>());
            Assert.Catch<ImplementationCannotBeInterfaceException>(() =>
                new DependenciesConfiguration().Register<IJustInterface, IJustInterface>());
            Assert.Catch<ImplementationIsNotAssignableException>(() =>
                new DependenciesConfiguration().Register<IJustInterface, AnotherClass>());
        }

        [Test]
        public void PoolTest()
        {
            DependenciesConfiguration config = new DependenciesConfiguration();
            config.Register<ForPoolTest, ForPoolTest>();
            DependencyProvider provider = new DependencyProvider(config, 5);
            
            ForPoolTest firstResolved = provider.Resolve<ForPoolTest>();
            provider.Resolve<ForPoolTest>();
            provider.Resolve<ForPoolTest>();
            provider.Resolve<ForPoolTest>();
            Assert.False(provider.Resolve<ForPoolTest>() == firstResolved);
            Assert.True(provider.Resolve<ForPoolTest>() == firstResolved);
        }

        [Test]
        public void OpenGenericTest()
        {
            GenericProvider<object> genericProvider = new GenericProvider<object>();
            Assert.NotNull(genericProvider.Provider.Resolve<object>());
        }
    }
}