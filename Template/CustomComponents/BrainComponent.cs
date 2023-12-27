using GameEngine.Constants;
using GameEngine.Enums;
using GameEngine.Models.ECS.Core;
using System.Collections.Generic;

namespace Template.Components
{
    public class BrainComponent : IComponent
    {
        public List<EntityStateEnum> possibleStates = new List<EntityStateEnum>();

        public EntityStateEnum State = EntityStateEnum.Wander;

        public EntityTypeEnum EntityType;

        public Entity Target = Globals.PlayerEntity;

        public int WanderRange = 5;

        public int WanderDestinationChangeInterval = 150; // Should equate to 5 seconds of wait time

        public int WanderDestinationChangeCounter = 0; // Counts up each tick... Duh

        public int PathStartDistance = 15; // Maximum distance for a path to start

        public int PathStopDistance = 0; // How close the entity can get to it's target before it stops

        public int LineOfSightMaxDistance = 20; // How close an entity has to be to spot the player with Line of Sight

        public int AttackDistance; // How close the entity has to be to attack the player
    }
}
