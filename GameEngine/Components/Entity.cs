using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using GameEngine.Core;
using GameEngine.Enums;
using GameEngine.Renderers;
using GameEngine.Handlers;

namespace GameEngine.Components
{
    public abstract class Entity : Component
    {
        public int Width => CurrentTexture.Width;

        public int Height => CurrentTexture.Height;

        public Vector2 Location = new (0, 0);

        public Vector2 Velocity = new(0, 0);

        public Direction Direction = Direction.Right;

        public static Dictionary<string, HashSet<Texture2D>> Textures = new ();

        public List<Texture2D> CurrentDirectionTextures;

        public Texture2D CurrentTexture;

        public Rectangle BoundingBox => new ((int)X, (int)Y, CurrentTexture.Width, CurrentTexture.Height);

        public bool RenderBoundingBox { get; set; }

        public float X { get => Location.X; set => Location.X = value; }

        public float Y { get => Location.Y; set => Location.Y = value; }

        public float VX { get => Velocity.X; set => Velocity.X = value; }

        public float VY { get => Velocity.Y; set => Velocity.Y = value; }

        public void Move(GameTime gameTime)
        {
            X += VX * (gameTime.ElapsedGameTime.Milliseconds / 16);
            Y += VY * (gameTime.ElapsedGameTime.Milliseconds / 16);
        }

        protected static HashSet<Texture2D> GetDirectionTextures(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up: return
                    Textures["Up"];

                case Direction.Left:
                case Direction.Right:
                    return Textures["Right"];

                case Direction.Down:
                    return Textures["Down"];
            };

            return Textures["Right"];
        }

        protected void DrawSprite(SpriteBatch spriteBatch)
        {
            if (CurrentTexture == null)
            {
                return;
            }

            SpriteEffects effect = Direction == Direction.Left ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            spriteBatch.Draw(CurrentTexture, Location,
                new Rectangle(0, 0, CurrentTexture.Width, CurrentTexture.Height), Color.White, 0,
                new Vector2(0, 0), 1.0f, effect, 0.0f);

            if (RenderBoundingBox)
            {
                spriteBatch.DrawRectangle(BoundingBox, Color.Red, 1);
            }
        }

        public bool CollidesWith(Entity entity)
        {
            return PerPixelCollision(this, entity);
        }

        protected bool PerPixelCollision(Entity entity1, Entity entity2)
        {
            // Get Color data of each Texture
            Color[] bitsA = new Color[entity1.Width * entity1.Height];
            entity1.CurrentTexture.GetData(bitsA);
            Color[] bitsB = new Color[entity2.Width * entity2.Height];
            entity2.CurrentTexture.GetData(bitsB);

            // Calculate the intersecting rectangle
            int x1 = Math.Max(entity1.BoundingBox.X, entity2.BoundingBox.X);
            int x2 = Math.Min(entity1.BoundingBox.X + entity1.BoundingBox.Width, entity2.BoundingBox.X + entity2.BoundingBox.Width);

            int y1 = Math.Max(entity1.BoundingBox.Y, entity2.BoundingBox.Y);
            int y2 = Math.Min(entity1.BoundingBox.Y + entity1.BoundingBox.Height, entity2.BoundingBox.Y + entity2.BoundingBox.Height);

            // For each single pixel in the intersecting rectangle
            for (int y = y1; y < y2; ++y)
            {
                for (int x = x1; x < x2; ++x)
                {
                    // Get the color from each texture
                    Color a = bitsA[(x - entity1.BoundingBox.X) + (y - entity1.BoundingBox.Y) * entity1.Width];
                    Color b = bitsB[(x - entity2.BoundingBox.X) + (y - entity2.BoundingBox.Y) * entity2.Width];

                    if (a.A != 0 && b.A != 0) // If both colors are not transparent (the alpha channel is not 0), then there is a collision
                    {
                        return true;
                    }
                }
            }
            // If no collision occurred by now, we're clear.
            return false;
        }
    }
}
