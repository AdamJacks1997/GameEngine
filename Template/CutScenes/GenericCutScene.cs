using GameEngine.Components;
using GameEngine.Handlers;
using System.Collections.Generic;
using System;
using Template.Components;
using GameEngine.Models.ECS.Core;
using System.Linq;
using GameEngine.Models.CutScenes;

namespace Template.CutScenes
{
    public class GenericCutScene
    {
        public string SceneName;

        public CutSceneStep Steps;

        public readonly List<Type> CameraComponentTypes = new List<Type>()
        {
            typeof(CameraFollowComponent),
            typeof(SceneComponent),
        };

        public readonly List<Type> CharacterComponentTypes = new List<Type>()
        {
            typeof(CharacterComponent),
            typeof(SceneComponent),
        };

        public virtual void Play()
        {

        }

        public Entity GetSingleEntityWithComponents(List<Type> componentTypes)
        {
            var entities = EntityHandler.GetWithComponents(componentTypes);

            return entities.SingleOrDefault(e => e.GetComponent<SceneComponent>().SceneName == SceneName);
        }

        public List<Entity> GetEntityListWithComponents(List<Type> componentTypes)
        {
            var entities = EntityHandler.GetWithComponents(componentTypes);

            return entities.Where(e => e.GetComponent<SceneComponent>().SceneName == SceneName).ToList();
        }
    }
}
