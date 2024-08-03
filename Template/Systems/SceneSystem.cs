using GameEngine.Systems;
using Microsoft.Xna.Framework;
using Template.Handlers;

namespace Template.Systems
{
    public class SceneSystem : IUpdateDuringCutSceneSystem
    {
        public void UpdateDuringCutScene(GameTime gameTime)
        {
            var isWaitingForMoveToComplete = CutSceneHandler.IsWaitingForMoveToComplete();

            if (isWaitingForMoveToComplete)
            {
                CutSceneHandler.Move(gameTime);
                
                return;
            }

            CutSceneHandler.NextStep();
        }
    }
}
