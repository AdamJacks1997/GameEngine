﻿using System;
using System.Collections.Generic;
using System.Linq;
using GameEngine.Core;

namespace GameEngine.Handlers
{
    public static class EntityHandler
    {
        private static readonly List<Entity> Entities = new List<Entity>();
        private static readonly Dictionary<Type, List<Entity>> EntitiesByComponentType = new Dictionary<Type, List<Entity>>();

        public static void Add(Entity entity)
        {
            Entities.Add(entity);
            UpdateEntitiesByComponentType(entity);
        }

        public static List<Entity> GetWithComponent<T>() where T : IComponent
        {
            var componentType = typeof(T);
            if (EntitiesByComponentType.TryGetValue(componentType, out var entities))
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
            
            IEnumerable<Entity> entities = Entities;

            foreach (var componentType in componentTypes)
            {
                if (EntitiesByComponentType.TryGetValue(componentType, out var componentEntities))
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
                if (!EntitiesByComponentType.TryGetValue(componentType, out var entities))
                {
                    entities = new List<Entity>();
                    EntitiesByComponentType.Add(componentType, entities);
                }

                entities.Add(entity);
            }
        }
    }
}
