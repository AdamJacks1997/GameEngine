using Microsoft.Xna.Framework;
using GameEngine.Handlers;
using GameEngine.Components;
using GameEngine.Models.ECS.Core;
using Template.Components;

namespace Template.Entities
{
    public class EnemySpawnerEntity : Entity
    {
        public EnemySpawnerEntity(Vector2 position)
        {
            var transform = AddComponent<TransformComponent>();
            AddComponent<EntitySpawnComponent>();

            transform.Position = position;

            EntityHandler.Add(this);
        }
    }
}
