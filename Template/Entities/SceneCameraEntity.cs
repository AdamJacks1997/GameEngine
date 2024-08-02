using Microsoft.Xna.Framework;
using GameEngine.Handlers;
using GameEngine.Components;
using GameEngine.Models.ECS.Core;
using Template.Components;
using System.Collections.Generic;

namespace Template.Entities
{
    public class SceneCameraEntity : Entity
    {
        public SceneCameraEntity(
            string sceneName,
            string entityName,
            List<Vector2> moves,
            Vector2 position)
        {
            var scene = AddComponent<SceneComponent>();
            var transform = AddComponent<TransformComponent>();
            AddComponent<CameraFollowComponent>();

            scene.SceneName = sceneName;
            scene.EntityName = entityName;
            scene.Moves = moves;

            transform.Position = position;
            transform.Size = new Point(16, 16);

            EntityHandler.Add(this);
        }
    }
}
