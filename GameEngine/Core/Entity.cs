﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine.Core
{
    public class Entity
    {
        private readonly Dictionary<Type, IComponent> _components = new Dictionary<Type, IComponent>();

        public T AddComponent<T>( ) where T : IComponent
        {
            var newComponent = (T)Activator.CreateInstance(typeof(T));

            _components.Add(typeof(T), newComponent);

            return newComponent;
        }

        public IEnumerable<IComponent> GetComponents()
        {
            return _components.Values;
        }

        public T GetComponent<T>() where T : IComponent
        {
            if (_components.TryGetValue(typeof(T), out var component))
            {
                return (T)component;
            }

            return default(T);
        }

        public bool HasComponent<T>() where T : IComponent
        {
            return _components.ContainsKey(typeof(T));
        }
    }
}
