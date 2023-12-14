using GameEngine.Constants;
using GameEngine.Handlers;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using Template.Components;

namespace Template.Systems
{
    public class CameraFollowSystem : IUpdateSystem
    {
        public void Update(GameTime gameTime)
        {
            var entities = EntityHandler.GetWithComponent<CameraFollowComponent>();

            entities.ForEach(entity =>
            {
                var transform = entity.GetComponent<TransformComponent>();

                Globals.CameraPosition = transform.Position;
            });
        }
    }
}
