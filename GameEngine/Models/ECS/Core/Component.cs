namespace GameEngine.Models.ECS.Core
{
    public abstract class IComponent
    {
        public Entity ParentEntity { get; set; }
    }
}
