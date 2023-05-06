using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GameEngine.Components;
using GameEngine.Handlers;

namespace MapEditor.Entities
{
    public class MouseEntity : Entity
    {
        public MouseEntity()
        {
            CurrentTexture = TextureHandler.GetEntity("Entities/Mouse");

            CollisionHandler.Add(this, "mouse");
        }

        public override void Update(GameTime gameTime)
        {
            Location = new Vector2(InputHandler.MousePosition.X - Width / 2, InputHandler.MousePosition.Y - Height / 2);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawSprite(spriteBatch);
        }
    }
}
