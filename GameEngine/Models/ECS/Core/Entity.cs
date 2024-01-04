using GameEngine.Components;
using System;
using System.Collections.Generic;

namespace GameEngine.Models.ECS.Core
{
    public class Entity
    {
        private readonly Dictionary<Type, IComponent> _components = new Dictionary<Type, IComponent>();

        public T AddComponent<T>() where T : IComponent
        {
            var newComponent = (T)Activator.CreateInstance(typeof(T));

            newComponent.ParentEntity = this;

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

            return default;
        }

        public bool HasComponent<T>() where T : IComponent
        {
            return _components.ContainsKey(typeof(T));
        }

        public void Destroy()
        {
            _components.Clear();
        }

        public TransformComponent Transform
        {
            get
            {
                return GetComponent<TransformComponent>();
            }
        }
    }
}
