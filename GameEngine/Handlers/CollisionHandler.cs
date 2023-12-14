using GameEngine.Constants;
using GameEngine.Models.ECS;
using System;
using System.Collections.Generic;

namespace GameEngine.Handlers
{
    public static class CollisionHandler
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

            if (xPosition > GameSettings.ScreenSize.X - width) // Collides Right
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

            if (yPosition > GameSettings.ScreenSize.Y - height) // Collides Bottom
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
            if (xPosition > GameSettings.ScreenSize.X - width)
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
            if (yPosition > GameSettings.ScreenSize.Y - height)
            {
                return true;
            }

            return false;
        }

        //public static bool AABB(Entity one, Entity two)
        //{
        //    return one.X < two.X + two.Width &&
        //           one.X + one.Width > two.X &&
        //           one.Y < two.Y + two.Height &&
        //           one.Y + one.Height > two.Y;
        //}

        //public static bool IsCollision(string source, string destination)
        //{
        //    Entity sourceSprite = EntityList[source];
        //    Entity destinationSprite = EntityList[destination];

        //    return sourceSprite.BoundingBox.Intersects(destinationSprite.BoundingBox);
        //}

        //public static bool IsPerPixelCollision(string source, string destination)
        //{
        //    Entity sourceEntity = EntityList[source];
        //    Entity destinationEntity = EntityList[destination];

        //    if (sourceEntity.BoundingBox.Intersects(destinationEntity.BoundingBox))
        //    {
        //        return sourceEntity.CollidesWith(destinationEntity);
        //    }

        //    return false;
        //}

        //protected bool PerPixelCollision(Entity entity1, Entity entity2)
        //{
        //    // Get Color data of each Texture
        //    Color[] bitsA = new Color[entity1.Width * entity1.Height];
        //    entity1.CurrentTexture.GetData(bitsA);
        //    Color[] bitsB = new Color[entity2.Width * entity2.Height];
        //    entity2.CurrentTexture.GetData(bitsB);

        //    // Calculate the intersecting rectangle
        //    int x1 = Math.Max(entity1.BoundingBox.X, entity2.BoundingBox.X);
        //    int x2 = Math.Min(entity1.BoundingBox.X + entity1.BoundingBox.Width, entity2.BoundingBox.X + entity2.BoundingBox.Width);

        //    int y1 = Math.Max(entity1.BoundingBox.Y, entity2.BoundingBox.Y);
        //    int y2 = Math.Min(entity1.BoundingBox.Y + entity1.BoundingBox.Height, entity2.BoundingBox.Y + entity2.BoundingBox.Height);

        //    // For each single pixel in the intersecting rectangle
        //    for (int y = y1; y < y2; ++y)
        //    {
        //        for (int x = x1; x < x2; ++x)
        //        {
        //            // Get the color from each texture
        //            Color a = bitsA[(x - entity1.BoundingBox.X) + (y - entity1.BoundingBox.Y) * entity1.Width];
        //            Color b = bitsB[(x - entity2.BoundingBox.X) + (y - entity2.BoundingBox.Y) * entity2.Width];

        //            if (a.A != 0 && b.A != 0) // If both colors are not transparent (the alpha channel is not 0), then there is a collision
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    // If no collision occurred by now, we're clear.
        //    return false;
        //}
    }
}
