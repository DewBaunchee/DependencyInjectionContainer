using Lib;

namespace Tests.Entity
{
    public interface IJustInterface
    {
    }

    public class JustClass : IJustInterface
    {
        public IAnotherInterface AnotherClass { get; }

        public JustClass(IAnotherInterface anotherClass)
        {
            AnotherClass = anotherClass;
        }
    }

    public interface IAnotherInterface
    {
    }

    public class AnotherClass : IAnotherInterface
    {
        public AnotherClass(IJustInterface justClass)
        {
        }
    }

    public class GenericArgument
    {
        
    }

    public class ForPoolTest
    {
        
    }

    public abstract class AbstractClass : IJustInterface
    {
        
    }

    public class GenericClass<T>
    {
        public T Something { get; set; }

        public GenericClass(T something)
        {
            Something = something;
        }
    }

    public class GenericProvider<T>
    {
        
        public DependencyProvider Provider { get; }
        public GenericProvider()
        {
            DependenciesConfiguration configuration = new DependenciesConfiguration();
            configuration.Register<T, T>();
            configuration.Register<GenericClass<T>, GenericClass<T>>();
            Provider = new DependencyProvider(configuration);
            
        }
    }
}