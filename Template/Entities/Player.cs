using Microsoft.Xna.Framework;
using GameEngine.Handlers;
using Template.Components;
using GameEngine.Models.ECS;

namespace Template.Entities
{
    public class Player : Entity
    {
        public Player(TextureHandler textureHandler)
        {

            //CollisionHandler.Add(this, "player");

            var animatedSprite = AddComponent<AnimatedSpriteComponent>();
            var transform = AddComponent<TransformComponent>();
            var velocity = AddComponent<VelocityComponent>();
            var collider = AddComponent<ColliderComponent>();
            AddComponent<PlayerControllerComponent>();

            animatedSprite.Textures.Add("Up", textureHandler.GetGroup("StorkUp"));
            animatedSprite.Textures.Add("Right", textureHandler.GetGroup("StorkRight"));
            animatedSprite.Textures.Add("Down", textureHandler.GetGroup("StorkDown"));

            transform.Position = new Vector2(100, 100);
            transform.Size = new Point(32, 32);

            velocity.Speed = 100f;

            collider.Bounds = transform.Bounds;

            EntityHandler.Add(this);
        }

        //public override void Update(GameTime gameTime)
        //{

        //    HandleInputs();

        //    if (CollisionHandler.IsPerPixelCollision("player", "collidable"))
        //    {
        //        RenderBoundingBox = true;
        //    }
        //    else
        //    {
        //        RenderBoundingBox = false;
        //    }

        //    Move(gameTime);
        //}
    }
}
