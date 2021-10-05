using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Lib.Exception;

namespace Lib
{
    public class DependencyProvider
    {
        private Dictionary<Type, Type> _dependencies;
        private Dictionary<Type, DependencyFactory> _factories;
        private Dictionary<UniqueId, Stack<Type>> _currentProducing;

        public DependencyProvider(DependenciesConfiguration dependencies)
        {
            Init(dependencies);
        }

        public T Resolve<T>()
        {
            return (T) Construct(new UniqueId(), typeof(T));
        }

        internal object Construct(UniqueId id, Type type)
        {
            CheckCyclingDependency(id, type);
            _currentProducing[id].Push(type);
            if (!_factories.ContainsKey(type))
                throw new CannotResolveSuchTypeException("Cannot resolve such type: " + type);
            return _factories[type].Produce(id);
        }

        private void CheckCyclingDependency(UniqueId id, Type type)
        {
            if (_currentProducing.ContainsKey(id))
            {
                List<Type> checkedTypes = new List<Type>();
                foreach (var producingType in _currentProducing[id])
                {
                    checkedTypes.Add(_dependencies[producingType]);
                    if (producingType == type)
                        throw new CyclingDependencyException("Detected cycling dependency: " + string.Join("->", checkedTypes));
                }
            }
            else
            {
                _currentProducing[id] = new Stack<Type>();
            }
        }

        internal bool CanConstruct(Type type)
        {
            return _dependencies.ContainsKey(type);
        }

        private void Init(DependenciesConfiguration dependencies)
        {
            _dependencies = dependencies.Dependencies.ToDictionary(pair => pair.Key, pair => pair.Value);
            _factories = new Dictionary<Type, DependencyFactory>();
            _currentProducing = new Dictionary<UniqueId, Stack<Type>>();
            foreach (var key in dependencies.Dependencies.Keys)
            {
                Type producingType = dependencies.Dependencies[key];
                _factories[key] = new DependencyFactory(producingType, this);
            }
        }
    }
}