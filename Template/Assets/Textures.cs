using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Template.Assets
{
    public class Textures
    {
        public static Texture2D Duck;

        public void LoadContent(ContentManager content)
        {
            Duck = content.Load<Texture2D>("pixelDuck");
        }
    }
}
