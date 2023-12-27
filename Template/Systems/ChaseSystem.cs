using GameEngine.Components;
using GameEngine.Constants;
using GameEngine.Enums;
using GameEngine.Globals;
using GameEngine.Handlers;
using GameEngine.Models.ECS.Core;
using GameEngine.Systems;
using GameEngineTools;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Template.Components;

namespace Template.Systems
{
    public class ChaseSystem : IUpdateSystem
    {
        private List<Entity> _entities;

        private Entity _player;

        private readonly List<Type> _componentTypes = new List<Type>()
        {
            typeof(BrainComponent),
        };

        public void Update(GameTime gameTime)
        {
            _entities = EntityHandler.GetWithComponents(_componentTypes);

            _player = Globals.PlayerEntity;

            var playerTransform = _player.GetComponent<TransformComponent>();

            _entities.ForEach(entity =>
            {
                var brain = entity.GetComponent<BrainComponent>();

                if (brain.State != EntityStateEnum.Chase)
                {
                    return;
                }

                var transform = entity.GetComponent<TransformComponent>();
                var velocity = entity.GetComponent<VelocityComponent>();

                var targetEntity = brain.Target;

                var targetTranform = targetEntity.GetComponent<TransformComponent>();

                velocity.DirectionVector = -Vector2.Normalize(transform.Position - targetTranform.Position);

                if (velocity.DirectionVector.X != 0 && velocity.DirectionVector.Y != 0)
                {
                    velocity.DirectionVector *= new Vector2(GameSettings.DiagnalSpeedMultiplier);
                }
            });
        }
    }
}
