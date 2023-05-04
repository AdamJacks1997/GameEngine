using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameEngine.Handlers
{
    public class TextureHandler
    {
        private readonly ContentManager _content;

        public static Dictionary<string, Texture2D> Loaded = new();

        public TextureHandler(ContentManager content)
        {
            _content = content;
        }

        public void Load(string texture)
        {
            Texture2D newTexture = _content.Load<Texture2D>(texture);
            Loaded.Add(texture, newTexture);
        }

        public static Texture2D Get(string texture)
        {
            return Loaded[texture];
        }
    }
}
