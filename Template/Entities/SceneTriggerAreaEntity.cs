using Microsoft.Xna.Framework;
using GameEngine.Handlers;
using GameEngine.Components;
using GameEngine.Models.ECS.Core;
using Template.Components;

namespace Template.Entities
{
    public class SceneTriggerAreaEntity : Entity
    {
        public SceneTriggerAreaEntity(
            Vector2 position,
            Point size,
            string SceneName)
        {
            var transform = AddComponent<TransformComponent>();
            var trigger = AddComponent<SceneTriggerComponent>();

            transform.Position = position;
            transform.Size = size;

            trigger.SceneName = SceneName;

            EntityHandler.Add(this);
        }
    }
}
