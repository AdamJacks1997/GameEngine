using GameEngine.Handlers;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using Template.Components;

namespace Template.Systems
{
    public class MovementSystem : IUpdateSystem
    {
        public void Update(GameTime gameTime)
        {
            var entities = EntityHandler.GetWithComponent<VelocityComponent>();

            entities.ForEach(entity =>
            {
                var transform = entity.GetComponent<TransformComponent>();
                var velocity = entity.GetComponent<VelocityComponent>();

                transform.PreviousPosition = transform.Position;

                if (velocity.Direction != Vector2.Zero)
                {
                    transform.Position += (velocity.Direction * velocity.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                }
            });
        }
    }
}
