using Microsoft.Xna.Framework;
using GameEngine.Handlers;
using GameEngine.Components;
using GameEngine.Constants;
using GameEngine.Models.ECS.Core;
using Template.Components;
using Template.Handlers;
using System;
using GameEngine.Globals;
using GameEngine.Enums;
using System.Collections.Generic;

namespace Template.Entities
{
    public class MeleeEnemyEntity : Entity
    {
        public MeleeEnemyEntity()
        {
            var transform = AddComponent<TransformComponent>();
            var velocity = AddComponent<VelocityComponent>();
            var sprite = AddComponent<SpriteComponent>();
            var attackBox = AddComponent<AttackBoxComponent>();
            var collider = AddComponent<ColliderComponent>();
            var pathController = AddComponent<PathControllerComponent>();
            var brain = AddComponent<BrainComponent>();

            transform.Position = new Vector2(166, 150);
            transform.Size = new Point(16, 16);

            velocity.Speed = 100f;

            sprite.Texture = TextureHandler.Get("Tiles");
            sprite.Offset = new Point(0, -(GameSettings.TileSize / 2) + 1);
            sprite.Source = new Rectangle(32, 128, GameSettings.TileSize, GameSettings.TileSize);
            sprite.Layer = 0.10045f;

            attackBox.Width = transform.Size.X - 4;
            attackBox.Height = transform.Size.Y - 2;
            attackBox.Offset = new Point(2, -(GameSettings.TileSize / 2) + 2);

            collider.Width = transform.Size.X / 2;
            collider.Height = transform.Size.Y + 1;
            collider.Offset = new Point(GameSettings.TileSize / 4, 0);

            pathController.PathHandler = new PathHandler();
            pathController.PathRefreshInterval = 10;

            brain.possibleStates.Add(EntityStateEnum.Wander);
            brain.possibleStates.Add(EntityStateEnum.FollowPath);
            brain.possibleStates.Add(EntityStateEnum.Chase);
            brain.possibleStates.Add(EntityStateEnum.MeleeAttack);

            brain.PathStartDistance = 15;
            brain.PathStopDistance = 1;
            brain.LineOfSightMaxDistance = 20;
            brain.AttackDistance = 1;

            EntityHandler.Add(this);
        }
    }
}
