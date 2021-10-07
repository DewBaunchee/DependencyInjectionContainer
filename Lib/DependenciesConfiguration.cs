using System;
using System.Collections.Concurrent;
using Lib.Exception;

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
                throw new CannotBeValueTypeException("Dependency type cannot be value type");
            if (implementationType.IsInterface)
                throw new ImplementationCannotBeInterfaceException("Implementation type cannot be interface");
            if (implementationType.IsAbstract)
                throw new ImplementationCannotBeAbstractException("Implementation type cannot be abstract");
            if (!implementationType.IsAssignableTo(dependencyType))
                throw new ImplementationIsNotAssignableException("Type " + implementationType + " is not assignable to " + dependencyType);

            Dependencies[dependencyType] = implementationType;
        }
    }
}