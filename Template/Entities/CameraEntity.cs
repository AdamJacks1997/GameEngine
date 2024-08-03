using Microsoft.Xna.Framework;
using GameEngine.Handlers;
using GameEngine.Components;
using GameEngine.Models.ECS.Core;
using GameEngine.Globals;

namespace Template.Entities
{
    public class CameraEntity : Entity
    {
        public CameraEntity(
            Vector2 position)
        {
            var transform = AddComponent<TransformComponent>();

            transform.Position = position;
            transform.Size = new Point((int)GameSettings.NativeSize.X, (int)GameSettings.NativeSize.Y);

            EntityHandler.Add(this);
        }
    }
}
