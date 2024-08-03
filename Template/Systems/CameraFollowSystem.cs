using GameEngine.Systems;
using Microsoft.Xna.Framework;
using GameEngine.Globals;

namespace Template.Systems
{
    public class CameraFollowSystem : IUpdateSystem
    {
        private float _smoothTransitionSpeed = 0.5f;
        private float _smoothTransitionDistance = 0;

        public void Update(GameTime gameTime)
        {
            Globals.CameraEntity.Transform.Position = Globals.PlayerEntity.Transform.Position;
        }

        //public void UpdateDuringCutScene(GameTime gameTime)
        //{
        //    if (Globals.CameraEntityPosition == Globals.CameraEntity.Transform.Position)
        //    {
        //        return;
        //    }

        //    smoothTransitionDistance += smoothTransitionSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

        //    // Ensure t stays between 0 and 1
        //    smoothTransitionDistance = MathHelper.Clamp(smoothTransitionDistance, 0f, 1f);

        //    Globals.CameraEntityPosition = Vector2.Lerp(Globals.CameraEntityPosition, Globals.CameraEntity.Transform.Position, smoothTransitionDistance);
        //}
    }
}
