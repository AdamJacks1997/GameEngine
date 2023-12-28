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
    public class AttackSystem : IUpdateSystem
    {
        private List<Entity> _entities;

        private Entity _player;

        private readonly List<Type> _componentTypes = new List<Type>()
        {
            typeof(AttackComponent),
        };

        public void Update(GameTime gameTime)
        {
            _entities = EntityHandler.GetWithComponents(_componentTypes);

            _player = Globals.PlayerEntity;

            var playerTransform = _player.GetComponent<TransformComponent>();

            _entities.ForEach(entity =>
            {
                var attack = entity.GetComponent<AttackComponent>();

                attack.LifeTime++;

                if (attack.LifeTime >= attack.LifeTimeLimit) 
                {
                    EntityHandler.Remove(entity);
                }
            });
        }
    }
}
