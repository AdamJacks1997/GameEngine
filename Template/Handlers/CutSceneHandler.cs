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
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Template.Handlers
{
    public static class CutSceneHandler
    {
        public static string SceneName;

        private static CutScene _cutScene;
        private static int _currentStep;

        private static List<Entity> _cameraEntities;
        private static List<Entity> _characterEntities;

        private static Entity _movingEntity;
        private static Vector2 _movingGoal;

        private static float _smoothTransitionSpeed = 0.5f;
        private static float _smoothTransitionDistance = 0;

        private static readonly List<Type> _cameraComponentTypes = new List<Type>()
        {
            typeof(SceneComponent),
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

            Globals.CutSceneActive = true;

            PlayCurrentStep();
        }

        public static bool IsWaitingForMoveToComplete()
        {
            var isWaiting = _movingEntity.Transform.Position != _movingGoal;

            if (!isWaiting)
            {
                _smoothTransitionDistance = 0;
            }

            return isWaiting;
        }

        public static void Move(GameTime gameTime) // TODO: This will be handled in an ECS System
        {
            _smoothTransitionDistance += _smoothTransitionSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Ensure t stays between 0 and 1
            _smoothTransitionDistance = MathHelper.Clamp(_smoothTransitionDistance, 0f, 1f);

            _movingEntity.Transform.Position = Vector2.Lerp(_movingEntity.Transform.Position, _movingGoal, _smoothTransitionDistance);
        }

        public static void NextStep()
        {
            _currentStep++;

            if (_cutScene.Steps.Count == _currentStep)
            {
                Globals.CutSceneActive = false;

                return;
            }

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
            var camera = Globals.PlayerEntity;
            
            if (_cameraEntities.Any(e => e.GetComponent<SceneComponent>().EntityName == step.EntityName))
            {
                camera = _cameraEntities.SingleOrDefault(e => e.GetComponent<SceneComponent>().EntityName == step.EntityName);
            }

            switch (step.Type)
            {
                case CutSceneStepTypeEnum.Move:
                    _movingEntity = Globals.CameraEntity;
                    _movingGoal = camera.Transform.Position;
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
                    _movingEntity = character;
                    _movingGoal = characterSceneComponent.Moves[characterSceneComponent.CurrentMove];
                    characterSceneComponent.CurrentMove++;
                    break;
                case CutSceneStepTypeEnum.Chat:
                    Debug.WriteLine(characterSceneComponent.Chats[characterSceneComponent.CurrentChat]);
                    characterSceneComponent.CurrentChat++;
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