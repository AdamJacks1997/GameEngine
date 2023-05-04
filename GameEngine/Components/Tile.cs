using GameEngine.Components.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameEngine.Core;
using GameEngine.Enums;
using GameEngine.Handlers;
using GameEngine.Renderers;
using Newtonsoft.Json;

namespace GameEngine.Components
{
    public class Tile : ITile
    {
        public Vector2 Position = new(0, 0);

        public Rectangle BoundingBox => new((int)X, (int)Y, Texture.Width, Texture.Height);

        [JsonProperty("TextureName")]
        public string TextureName;

        public Texture2D Texture => TextureHandler.Get(TextureName);

        public bool RenderBoundingBox { get; set; }

        public float X
        {
            get => Position.X;
            set => Position.X = value;
        }

        public float Y
        {
            get => Position.Y;
            set => Position.Y = value;
        }

        public Vector2 Location => new(Position.X * Texture.Width, Position.Y * Texture.Height);

        public CollidableType CollidableType { get; set; } = CollidableType.None;
        

        public void DrawSprite(SpriteBatch spriteBatch)
        {
            if (Texture == null)
            {
                return;
            }

            spriteBatch.Draw(Texture, Location,
                new Rectangle(0, 0, Texture.Width, Texture.Height), Color.White, 0,
                new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.0f);

            if (RenderBoundingBox && CollidableType != CollidableType.None)
            {
                spriteBatch.DrawRectangle(BoundingBox, Color.Red, 3);
            }
        }
    }
}
