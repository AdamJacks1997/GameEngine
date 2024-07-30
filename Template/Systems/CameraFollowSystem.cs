using GameEngine.Systems;
using Microsoft.Xna.Framework;
using GameEngine.Globals;

namespace Template.Systems
{
    public class CameraFollowSystem : IUpdateSystem, IUpdateDuringCutSceneSystem
    {
        private float smoothTransitionSpeed = 0.5f;
        private float smoothTransitionDistance = 0;

        public void Update(GameTime gameTime)
        {
            Globals.CameraEntityPosition = Globals.CameraEntity.Transform.Position;
        }

        public void UpdateDuringCutScene(GameTime gameTime)
        {
            //Globals.CameraEntityPosition = Globals.CameraEntity.Transform.Position;
            // Increment the interpolation factor

            if (Globals.CameraEntityPosition == Globals.CameraEntity.Transform.Position)
            {
                return;
            }

            smoothTransitionDistance += smoothTransitionSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Ensure t stays between 0 and 1
            smoothTransitionDistance = MathHelper.Clamp(smoothTransitionDistance, 0f, 1f);

            Globals.CameraEntityPosition = Vector2.Lerp(Globals.CameraEntityPosition, Globals.CameraEntity.Transform.Position, smoothTransitionDistance);
        }
    }
}
