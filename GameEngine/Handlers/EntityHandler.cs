using System;
using System.Collections.Generic;
using System.Linq;
using GameEngine.Enums;
using GameEngine.Models.ECS.Core;

namespace GameEngine.Handlers
{
    public static class EntityHandler
    {
        private static readonly List<Entity> _entities = new List<Entity>();
        private static readonly Dictionary<Type, List<Entity>> _entitiesByComponentType = new Dictionary<Type, List<Entity>>();

        public static void Add(Entity entity)
        {
            _entities.Add(entity);
            UpdateEntitiesByComponentType(entity);
        }

        //public static Entity GetWithType(EntityType type) // this was silly, this is less efficient than getting by type
        //{
        //    return _entities.Where(e => e.Type == type).FirstOrDefault();
        //}

        public static List<Entity> GetWithComponent<T>() where T : IComponent
        {
            var componentType = typeof(T);
            if (_entitiesByComponentType.TryGetValue(componentType, out var entities))
            {
                return entities;
            }

            return new List<Entity>();
        }

        public static List<Entity> GetWithComponents(List<Type> componentTypes)
        {
            List<Entity> result = new List<Entity>();

            if (componentTypes.Count == 0)
            {
                return result;
            }
            
            IEnumerable<Entity> entities = _entities;

            foreach (var componentType in componentTypes)
            {
                if (_entitiesByComponentType.TryGetValue(componentType, out var componentEntities))
                {
                    entities = entities.Intersect(componentEntities);
                }
                else
                {
                    return result;
                }
            }

            result.AddRange(entities);

            return result;
        }

        private static void UpdateEntitiesByComponentType(Entity entity)
        {
            foreach (var component in entity.GetComponents())
            {
                var componentType = component.GetType();
                if (!_entitiesByComponentType.TryGetValue(componentType, out var entities))
                {
                    entities = new List<Entity>();
                    _entitiesByComponentType.Add(componentType, entities);
                }

                entities.Add(entity);
            }
        }
    }
}
