using GameEngine.Enums;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameEngine.Components.Interfaces
{
    public class ITile
    {
        float X { get; set; }

        float Y { get; set; }

        string TextureName;

        CollidableType CollidableType { get; set; } = CollidableType.None;
    }
}
