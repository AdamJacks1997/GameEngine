using Microsoft.Xna.Framework;
using GameEngine.Handlers;
using GameEngine.Components;
using GameEngine.Models.ECS.Core;
using Template.Components;
using GameEngine.Globals;
using System.Collections.Generic;

namespace Template.Entities
{
    public class SceneCharacterEntity : Entity
    {
        public SceneCharacterEntity(
            string sceneName,
            string entityName,
            List<Vector2> moves,
            List<string> chats,
            Vector2 position)
        {
            var scene = AddComponent<SceneComponent>();
            var transform = AddComponent<TransformComponent>();
            var velocity = AddComponent<VelocityComponent>();
            var sprite = AddComponent<SpriteComponent>();

            scene.SceneName = sceneName;
            scene.EntityName = entityName;
            scene.Moves = moves;
            scene.Chats = chats;

            transform.Position = position;
            transform.Size = new Point(16, 16);

            velocity.Speed = 50f;

            sprite.Texture = TextureHandler.Get("Tiles");
            sprite.Offset = new Point(0, -(GameSettings.TileSize / 2) + 1);
            sprite.Source = new Rectangle(0 * 16, 7 * 16, GameSettings.TileSize, GameSettings.TileSize);
            sprite.Layer = 0.10045f;

            EntityHandler.Add(this);
        }
    }
}
