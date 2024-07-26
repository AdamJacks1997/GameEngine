using System;
using System.Collections.Generic;
using System.Linq;
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

        //public static void Remove(Entity entity)
        //{
        //    entity.ClearComponents();

        //    _entitiesByComponentType.ToList().ForEach(entityList =>
        //    {

        //        if (entityList.Value.Contains(entity)) // TODO this is just for testing and should be removed
        //        {
        //            var test = "";
        //        }

        //        entityList.Value.Remove(entity);

        //        if (entityList.Value.Contains(entity)) // TODO this is just for testing and should be removed
        //        {
        //            var test = "";
        //        }
        //    });

        //    _entities.Remove(entity);

        //    entity = null;
        //}

        public static void Remove(Entity entity) // ChatGPT generated this because I wasn't happy with my current Remove method, unsure if this works the same
        {
            entity.ClearComponents();

            // Remove the entity from the _entities list
            _entities.Remove(entity);

            // Iterate over the dictionary and remove the entity from each list
            foreach (var entityList in _entitiesByComponentType.Values)
            {
                entityList.Remove(entity);
            }
        }

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

        private static IComponent GetComponentFromEntity(Entity entity, Type componentType)
        {
            var method = entity.GetType().GetMethod("GetComponent").MakeGenericMethod(componentType);
            return (IComponent)method.Invoke(entity, null);
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
