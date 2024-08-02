using GameEngine.Components;
using GameEngine.Handlers;
using System.Collections.Generic;
using System;
using Template.Components;
using GameEngine.Models.ECS.Core;
using System.Linq;
using GameEngine.Models.CutScenes;
using System.IO;
using Newtonsoft.Json;
using GameEngine.Enums;
using GameEngine.Globals;

namespace Template.Handlers
{
    public static class CutSceneHandler
    {
        public static string SceneName;

        private static CutScene _cutScene;
        private static int _currentStep;

        private static List<Entity> _cameraEntities;
        private static List<Entity> _characterEntities;

        private static readonly List<Type> _cameraComponentTypes = new List<Type>()
        {
            typeof(SceneComponent),
            typeof(CameraFollowComponent),
        };

        private static readonly List<Type> _characterComponentTypes = new List<Type>()
        {
            typeof(SceneComponent),
            typeof(VelocityComponent),
        };

        public static void Play(string sceneName)
        {
            SceneName = sceneName;

            _currentStep = 0;

            _cameraEntities = GetEntityListWithComponents(_cameraComponentTypes);
            _characterEntities = GetEntityListWithComponents(_characterComponentTypes);

            _cutScene = PopulateSteps();

            PlayCurrentStep();
            NextStep();
        }

        public static void NextStep()
        {
            _currentStep++;

            PlayCurrentStep();
        }

        private static void PlayCurrentStep()
        {
            var step = _cutScene.Steps[_currentStep];

            switch (step.EntityType)
            {
                case SceneEntityType.Camera:
                    HandleCamera(step);
                    break;
                case SceneEntityType.Character:
                    HandleCharacter(step);
                    break;
            }
        }

        private static void HandleCamera(CutSceneStep step)
        {
            var camera = _cameraEntities.SingleOrDefault(e => e.GetComponent<SceneComponent>().EntityName == step.EntityName);
            switch (step.Type)
            {
                case CutSceneStepTypeEnum.Move:
                    Globals.CameraEntity = camera;
                    break;
            }
        }

        private static void HandleCharacter(CutSceneStep step)
        {
            var character = _characterEntities.SingleOrDefault(e => e.GetComponent<SceneComponent>().EntityName == step.EntityName);
            var characterSceneComponent = character.GetComponent<SceneComponent>();

            switch (step.Type)
            {
                case CutSceneStepTypeEnum.Move:
                    character.Transform.Position = characterSceneComponent.Moves[0];
                    break;
                case CutSceneStepTypeEnum.Chat:

                    break;
            }
        }

        private static List<Entity> GetEntityListWithComponents(List<Type> componentTypes)
        {
            var entities = EntityHandler.GetWithComponents(componentTypes);

            return entities.Where(e => e.GetComponent<SceneComponent>().SceneName == SceneName).ToList();
        }

        private static CutScene PopulateSteps()
        {
            var cutSceneJson = LoadFile("../../../Map/SceneJson/", $"{SceneName}.json");

            var cutScene = JsonConvert.DeserializeObject<CutScene>(cutSceneJson);

            return cutScene;
        }

        private static string LoadFile(string path, string name)
        {
            string mapFilePath = Path.Combine(path, name);
            var reader = new StreamReader(mapFilePath);

            return reader.ReadToEnd();
        }
    }
}