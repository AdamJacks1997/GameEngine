using GameEngine.Models.ECS.Core;
using Template.Entities;

namespace Template.Components
{
    public class FootComponent : IComponent
    {
        public int FootPrintCount = 0;

        public int MaxFootPrintCount = 20;

        public int CreationDistance = 2;

        public FootPrintEntity LastFootPrint;
    }
}
