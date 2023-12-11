using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using GameEngine.Models.ECS;

namespace Template.Components
{
    public class AnimatedSpriteComponent : IComponent
    {
        public Dictionary<string, List<Texture2D>> Textures = new Dictionary<string, List<Texture2D>>();

        public int CurrentFrame = 0;

        public Texture2D CurrentTexture;

        public bool Play = true; // TODO This should be false by default and set when an entity moves

        public int InternalTimer = 0;

        public int AnimationSpeed = 10;
    }
}
