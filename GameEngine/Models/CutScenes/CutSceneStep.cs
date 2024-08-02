using GameEngine.Enums;

namespace GameEngine.Models.CutScenes
{
    public class CutSceneStep
    {
        public CutSceneStepTypeEnum Type { get; set; }

        public SceneEntityType EntityType { get; set; }

        public string EntityName { get; set; }
    }
}
