using System.Collections.Generic;

namespace GameEngine.Models.CutScenes
{
    public class CutScene
    {
        public List<string> Cameras { get; set; }
        public List<string> Characters { get; set; }
        public List<CutSceneStep> Steps { get; set; }
    }
}
