using GameEngine.Models.ECS.Core;
using GameEngineTools;

namespace Template.Components
{
    public class EntitySpawnComponent : IComponent
    {
        public int SpawnInterval { get; set; } = Randoms.IntBetween(120, 420);
        //public int SpawnInterval { get; set; } = Randoms.IntBetween(60, 61); // FAST MODE

        public int SpawnCounter { get; set; } = 0;
    }
}
