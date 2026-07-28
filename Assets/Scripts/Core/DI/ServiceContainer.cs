using System;
using System.Collections.Generic;

namespace Core.DI
{
    public sealed class ServiceContainer : IContainer
    {
        private readonly Dictionary<Type, object> _instances = new Dictionary<Type, object>();
        private readonly Dictionary<Type, Type> _registrations = new Dictionary<Type, Type>();

        public void Register<TAbstraction, TImplementation>() where TImplementation : TAbstraction, new()
        {
            _registrations[typeof(TAbstraction)] = typeof(TImplementation);
        }

        public void Register<TAbstraction>(TAbstraction instance)
        {
            _instances[typeof(TAbstraction)] = instance;
        }

        public T Resolve<T>()
        {
            Type abstractionType = typeof(T);

            if (_instances.TryGetValue(abstractionType, out object instance))
            {
                return (T)instance;
            }

            if (_registrations.TryGetValue(abstractionType, out Type implementationType))
            {
                object created = Activator.CreateInstance(implementationType);
                _instances[abstractionType] = created;
                return (T)created;
            }

            throw new InvalidOperationException($"No registration found for type '{abstractionType.FullName}'.");
        }
    }
}
