using Microsoft.Xna.Framework;

namespace GameEngine.Systems
{
    public interface IUpdateDuringCutSceneSystem : ISystem
    {
        void UpdateDuringCutScene(GameTime gameTime);
    }
}
