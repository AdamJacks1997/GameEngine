using Microsoft.Xna.Framework;

namespace GameEngine.Systems
{
    public interface IUpdateSystem : ISystem
    {
        void Update(GameTime gameTime);
    }
}
