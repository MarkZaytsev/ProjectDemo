using System;
using System.Collections.Generic;

namespace Common.FrostLib.Services
{
    public class ServiceLocator : Singleton<ServiceLocator>
    {
        private readonly IDictionary<Type, object> _services = new Dictionary<Type, object>();

        public T Get<T>()
        {
            var type = typeof(T);
            return _services.ContainsKey(type) ? (T)_services[type] : default(T);
        }

        public void Provide<T>(T service)
        {
            Remove<T>();
            _services.Add(typeof(T), service);
        }

        public void Remove<T>()
        {
            var service = Get<T>();
            if(service == null)
                return;

            _services.Remove(typeof(T));

            if(service is IDisposable disposable)
                disposable.Dispose();
        }

        public void ResetAll()
        {
            foreach (var pair in _services)
            {
                if (pair.Value is IDisposable disposable)
                    disposable.Dispose();
            }

            _services.Clear();
        }
    }
}