using GameEngine.Components;
using GameEngine.Core;
using System.Collections.Generic;

namespace GameEngine.Handlers
{
    public class CollisionHandler
    {
        private static readonly Dictionary<string, Entity> EntityList = new ();

        public static void Add(Entity entity, string name)
        {
            if (!EntityList.ContainsKey(name))
            {
                EntityList.Add(name, entity);
            }
        }

        public static void Remove(Entity entity, string name)
        {
            if (EntityList.ContainsKey(name))
            {
                EntityList.Remove(name);
            }
        }

        public static bool WithScreenHorizontal(float xPosition, int width)
        {
            if (xPosition < 0) // Collides Left
            {
                return true;
            }

            if (xPosition > Constants.ScreenWidth - width) // Collides Right
            {
                return true;
            }

            return false;
        }

        public static bool WithScreenVertical(float yPosition, int height)
        {
            if (yPosition < 0) // Collides Top
            {
                return true;
            }

            if (yPosition > Constants.ScreenHeight - height) // Collides Bottom
            {
                return true;
            }

            return false;
        }

        public static bool WithScreenLeft(float xPosition, int width)
        {
            if (xPosition < 0)
            {
                return true;
            }

            return false;
        }
        public static bool WithScreenRight(float xPosition, int width)
        {
            if (xPosition > Constants.ScreenWidth - width)
            {
                return true;
            }

            return false;
        }

        public static bool WithScreenTop(float yPosition, int height)
        {
            if (yPosition < 0)
            {
                return true;
            }

            return false;
        }

        public static bool WithScreenBottom(float yPosition, int height)
        {
            if (yPosition > Constants.ScreenHeight - height)
            {
                return true;
            }

            return false;
        }

        public static bool AABB(Entity one, Entity two)
        {
            return one.X < two.X + two.Width &&
                   one.X + one.Width > two.X &&
                   one.Y < two.Y + two.Height &&
                   one.Y + one.Height > two.Y;
        }

        public static bool IsPerPixelCollision(string source, string destination)
        {
            Entity sourceEntity = EntityList[source];
            Entity destinationEntity = EntityList[destination];

            if (sourceEntity.BoundingBox.Intersects(destinationEntity.BoundingBox))
            {
                return sourceEntity.CollidesWith(destinationEntity);
            }

            return false;
        }
    }
}
