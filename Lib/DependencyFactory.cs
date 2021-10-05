using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using Lib.Exception;

namespace Lib
{
    public class DependencyFactory
    {
        private readonly ConstructorInfo _constructorInfo;
        private readonly DependencyProvider _provider;

        public DependencyFactory(Type type, DependencyProvider provider)
        {
            _provider = provider;
            _constructorInfo = GetBestConstructor(type);
        }

        public object Produce(UniqueId id)
        {
            return _constructorInfo.Invoke(
                _constructorInfo.GetParameters()
                    .Select(info => _provider.Construct(id, info.ParameterType))
                    .ToArray()
            );
        }

        private ConstructorInfo GetBestConstructor(Type type)
        {
            ConstructorInfo bestConstructor = null;
            List<ConstructorInfo> constructors = type.GetConstructors()
                .ToList();
            constructors.Sort((a, b) =>
                a.GetParameters().Length.CompareTo(b.GetParameters().Length)
            );
            
            constructors.ForEach(constructor =>
            {
                if (constructor.GetParameters().All(info => _provider.CanConstruct(info.ParameterType)))
                    bestConstructor = constructor;
            });

            if (bestConstructor == null)
                throw new CannotFindSuitableConstructorException("Cannot find suitable constructor: " + type);

            return bestConstructor;
        }
    }
}