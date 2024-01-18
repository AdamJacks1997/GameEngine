using Microsoft.Xna.Framework;
using GameEngine.Handlers;
using GameEngine.Components;
using GameEngine.Models.ECS.Core;
using Template.Components;
using Template.Handlers;
using GameEngine.Globals;
using GameEngine.Enums;

namespace Template.Entities
{
    public class MeleeEnemyEntity : Entity
    {
        public MeleeEnemyEntity(Vector2 position)
        {
            var transform = AddComponent<TransformComponent>();
            var velocity = AddComponent<VelocityComponent>();
            var sprite = AddComponent<SpriteComponent>();
            var hurtBox = AddComponent<HurtBoxComponent>();
            var collider = AddComponent<ColliderComponent>();
            var pathController = AddComponent<PathControllerComponent>();
            var brain = AddComponent<BrainComponent>();

            transform.Position = position;
            transform.Size = new Point(16, 16);

            velocity.Speed = 50f;

            sprite.Texture = TextureHandler.Get("Tiles");
            sprite.Offset = new Point(0, -(GameSettings.TileSize / 2) + 1);
            sprite.Source = new Rectangle(1 * 16, 9 * 16, GameSettings.TileSize, GameSettings.TileSize);
            sprite.Layer = 0.10045f;

            hurtBox.Width = transform.Size.X - 4;
            hurtBox.Height = transform.Size.Y - 2;
            hurtBox.Offset = new Point(2, -(GameSettings.TileSize / 2) + 2);

            collider.Width = transform.Size.X;
            collider.Height = transform.Size.Y;
            collider.Offset = new Point(0, 0);

            pathController.PathHandler = new PathHandler();
            pathController.PathRefreshInterval = 10;

            brain.possibleStates.Add(EntityStateEnum.Wander);
            brain.possibleStates.Add(EntityStateEnum.FollowPath);
            brain.possibleStates.Add(EntityStateEnum.Chase);
            brain.possibleStates.Add(EntityStateEnum.MeleeAttack);

            brain.PathStartDistance = 15;
            brain.PathStopDistance = 1;
            brain.LineOfSightMaxDistance = 20;
            brain.AttackDistance = 0;

            EntityHandler.Add(this);
        }
    }
}
