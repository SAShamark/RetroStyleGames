using System;
using System.Collections.Generic;
using UnityEngine;

namespace Services
{
    public class ServiceLocator : IServiceLocator
    {
        public static IServiceLocator SharedInstance => _instance ??= new ServiceLocator();
        private static IServiceLocator _instance;

        private readonly Dictionary<Type, object> _services;

        private ServiceLocator()
        {
            _services = new Dictionary<Type, object>();
        }

        public void Register<T>(T data)
        {
            if (_services.ContainsKey(typeof(T)))
            {
                Debug.LogError($"Service {typeof(T).Name} already registered");
            }
            else
            {
                _services[typeof(T)] = data;
                Debug.Log($"Service {typeof(T).Name} was registered");
            }
        }

        public void Unregister<T>()
        {
            if (!_services.ContainsKey(typeof(T)))
            {
                Debug.LogError($"Service {typeof(T).Name} already removed");
            }
            else
            {
                _services.Remove(typeof(T));
            }
        }

        public T Resolve<T>()
        {
            if (!_services.ContainsKey(typeof(T)))
            {
                Debug.LogError($"Service {typeof(T).Name} didn't registered");
            }
            else
            {
                return (T)_services[typeof(T)];
            }

            return default;
        }
    }
}