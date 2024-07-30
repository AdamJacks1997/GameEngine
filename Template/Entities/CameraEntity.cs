using Microsoft.Xna.Framework;
using GameEngine.Handlers;
using GameEngine.Components;
using GameEngine.Models.ECS.Core;
using Template.Components;

namespace Template.Entities
{
    public class CameraEntity : Entity
    {
        public CameraEntity(
            string sceneName,
            Vector2 position)
        {
            var scene = AddComponent<SceneComponent>();
            var transform = AddComponent<TransformComponent>();
            AddComponent<CameraFollowComponent>();

            scene.SceneName = sceneName;

            transform.Position = position;
            transform.Size = new Point(16, 16);

            EntityHandler.Add(this);
        }
    }
}
