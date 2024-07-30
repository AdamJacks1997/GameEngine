
using Template.CutScenes;
using System.Collections.Generic;

namespace Template.Helpers
{
    public static class SceneHelper
    {
        private static Dictionary<string, GenericCutScene> _scenes = new Dictionary<string, GenericCutScene>();

        public static void LoadScenesByLevel(string levelName)
        {
            _scenes = new Dictionary<string, GenericCutScene>();

            switch (levelName)
            {
                case "Start":
                    _scenes.Add("AryanRaceStart", new AryanRaceStartScene());
                    break;
            }
        }

        public static GenericCutScene GetSceneByName(string sceneName)
        {
            return _scenes[sceneName];
        }
    }
}
