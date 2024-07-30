using GameEngine.Globals;
using GameEngine.Handlers;
using GameEngine.Models.ECS.Core;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Template.Components;
using Template.Helpers;

namespace Template.Systems
{
    public class SceneTriggerSystem : IUpdateSystem
    {
        private Entity _player;
        private List<Entity> _sceneTriggerAreaEntities;

        public void Update(GameTime gameTime)
        {
            _player = Globals.PlayerEntity;

            _sceneTriggerAreaEntities = EntityHandler.GetWithComponent<SceneTriggerComponent>();

            _sceneTriggerAreaEntities.ForEach(entity =>
            {
                if (_player.Transform.Bounds.Intersects(entity.Transform.Bounds))
                {
                    var sceneTrigger = entity.GetComponent<SceneTriggerComponent>();

                    if (sceneTrigger.Used)
                    {
                        return;
                    }

                    Globals.CutSceneActive = true;

                    sceneTrigger.Used = true;

                    var scene = SceneHelper.GetSceneByName(sceneTrigger.SceneName);

                    scene.Play();
                }
            });
        }
    }
}
