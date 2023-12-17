using GameEngine.Components;
using GameEngine.Models;
using GameEngine.Models.ECS.Core;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameEngine.Handlers
{
    public class CollisionHandler
    {
        public Quadtree CollisionQuadtree;
        public Rectangle Bounds;

        public void Init(Rectangle bounds)
        {
            CollisionQuadtree = new Quadtree(bounds);
            Bounds = bounds;
        }

        public void Add(Entity collidable)
        {
            CollisionQuadtree.Insert(collidable);
        }

        public void Remove(Entity collidable)
        {
            CollisionQuadtree.Remove(collidable);
        }

        public void Update(List<Entity> collidables)
        {
            foreach (Entity collidable in collidables)
            {
                CollisionQuadtree.Remove(collidable);

                CollisionQuadtree.Insert(collidable);
            }
        }
        
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
