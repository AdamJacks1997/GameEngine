using GameEngine.Models.ECS.Core;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameEngine.Components
{
    public class CharacterComponent : IComponent
    {
        public string Name { get; set; }
        public List<Vector2> Movements { get; set; }
    }
}
