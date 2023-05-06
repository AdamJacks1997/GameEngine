using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GameEngine.Components;
using GameEngine.Handlers;
using System.Xml.Linq;

namespace MapEditor.Entities
{
    public class TileEntity : Entity
    {
        public TileEntity(Texture2D texture, Vector2 location)
        {
            CurrentTexture = texture;
            Location = location;
            CollisionHandler.Add(this, texture.Name);
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawSprite(spriteBatch);
        }
    }
}
