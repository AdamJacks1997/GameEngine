using GameEngine.Models.ECS.Core;

namespace Template.Components
{
    public class SceneTriggerComponent : IComponent
    {
        public string SceneName { get; set; }

        public bool Used { get; set; }
    }
}
