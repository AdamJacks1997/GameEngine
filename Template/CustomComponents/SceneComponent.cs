using GameEngine.Models.ECS.Core;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Template.Components
{
    public class SceneComponent : IComponent
    {
        public string SceneName { get; set; }

        public string EntityName { get; set; }

        public int CurrentMove { get; set; } = 0;

        public int CurrentChat { get; set; } = 0;

        public List<Vector2> Moves { get; set; }

        public List<string> Chats { get; set; }
    }
}
