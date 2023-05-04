using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GameEngine.Components;
using GameEngine.Handlers;

namespace Template.Entities
{
    public class CollidableExample : Entity
    {
        public CollidableExample()
        {
            CurrentTexture = TextureHandler.Get("Duck");
            Location = new Vector2(250, 100);

            CollisionHandler.Add(this, "collidable");
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawSprite(spriteBatch);
        }
    }
}
