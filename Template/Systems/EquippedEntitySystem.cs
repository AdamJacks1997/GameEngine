using GameEngine.Components;
using GameEngine.Globals;
using GameEngine.Handlers;
using GameEngine.Models.ECS.Core;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Template.Components;

namespace Template.Systems
{
    public class EquippedEntitySystem : IUpdateSystem
    {
        private List<Entity> _entities;

        private readonly List<Type> _componentTypes = new List<Type>()
        {
            typeof(EquippedEntityComponent),
        };

        public void Update(GameTime gameTime)
        {
            _entities = EntityHandler.GetWithComponents(_componentTypes);

            _entities.ForEach(entity =>
            {
                var equippedEntityComponent = entity.GetComponent<EquippedEntityComponent>();

                if (equippedEntityComponent.Entity == null)
                {
                    return;
                }

                var holder = equippedEntityComponent.ParentEntity;
                var item = equippedEntityComponent.Entity;

                item.Transform.Position = holder.Transform.Position + equippedEntityComponent.Offset.ToVector2();
                item.Sprite.Layer = holder.Sprite.Layer + 0.000001f;
            });
        }
    }
}
