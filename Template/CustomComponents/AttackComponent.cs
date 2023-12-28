using GameEngine.Models.ECS.Core;

namespace Template.Components
{
    public class AttackComponent : IComponent
    {
        public int LifeTime = 0;

        public int LifeTimeLimit;

        public int Damage;
    }
}
