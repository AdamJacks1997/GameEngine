using GameEngine.Models.ECS.Core;

namespace Template.Components
{
    public class WeaponComponent : IComponent
    {
        public Entity Owner { get; set; }
    }
}
