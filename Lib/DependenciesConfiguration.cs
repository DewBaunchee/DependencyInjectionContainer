using System;
using System.Collections.Concurrent;

namespace Lib
{
    public class DependenciesConfiguration
    {

        internal ConcurrentDictionary<Type, Type> Dependencies { get; } = new();

        public void Register<TDependency, TImplementation>()
        {
            Type dependencyType = typeof(TDependency);
            Type implementationType = typeof(TImplementation);

            if (dependencyType.IsValueType)
                throw new ArgumentException("Dependency type cannot be value type");
            if (implementationType.IsAbstract)
                throw new ArgumentException("Implementation type cannot be abstract");
            if (implementationType.IsInterface)
                throw new ArgumentException("Implementation type cannot be interface");
            if (!implementationType.IsAssignableTo(dependencyType))
                throw new ArgumentException("Type " + implementationType + " is not assignable to " + dependencyType);

            Dependencies[dependencyType] = implementationType;
        }
    }
}