using GameEngine.Models.ECS.Core;
using GameEngine.Globals;
using System.Linq;
using GameEngine.Components;

namespace Template.CutScenes
{
    public class AryanRaceStartScene : GenericCutScene
    {
        private Entity _camera;
        private Entity _german;

        public AryanRaceStartScene()
        {
            SceneName = "AryanRaceStart";

            GetEntities();

            PopulateSteps();
        }

        public override void Play()
        {
            Globals.CameraEntity = _camera;
        }

        private void GetEntities()
        {
            _camera = GetSingleEntityWithComponents(CameraComponentTypes);

            var characters = GetEntityListWithComponents(CharacterComponentTypes);

            _german = characters.SingleOrDefault(c => c.GetComponent<CharacterComponent>().Name == "German");
        }

        private void PopulateSteps()
        {

        }
    }
}
