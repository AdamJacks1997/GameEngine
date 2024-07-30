using GameEngine.Handlers;
using GameEngine.Models.ECS.Core;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Template.Components;
using Template.Entities;

namespace Template.Systems
{
    public class FootPrintSystem : IUpdateSystem
    {
        private List<Entity> _footEntities;
        private List<Entity> _footPrintEntities;
        private int _footPrintCount;

        public void Update(GameTime gameTime)
        {
            CreateFootPrints();

            AgeFootPrints();
        }

        private void CreateFootPrints()
        {
            _footEntities = EntityHandler.GetWithComponent<FootComponent>();

            _footEntities.ForEach(entity =>
            {
                var foot = entity.GetComponent<FootComponent>();

                if (foot.FootPrintCount == 0)
                {
                    CreateNewFootPrint(foot);
                }

                if (Vector2.Distance(foot.LastFootPrint.Transform.GridPosition.ToVector2(), foot.ParentEntity.Transform.GridPosition.ToVector2()) > foot.CreationDistance)
                {
                    CreateNewFootPrint(foot);
                }
            });
        }

        private void CreateNewFootPrint(FootComponent foot)
        {
            var newFootPrint = new FootPrintEntity(foot, foot.ParentEntity.Transform.Position, foot.FootPrintCount);
            
            //foot.Direction = THE DIRECTION DUMBASS 
            foot.LastFootPrint = newFootPrint;
            foot.FootPrintCount++;
        }

        private void AgeFootPrints()
        {
            _footPrintEntities = EntityHandler.GetWithComponent<FootPrintComponent>();

            Entity footPrintToRemove = null;

            _footPrintEntities.ForEach(footPrintEntity =>
            {
                var footPrint = footPrintEntity.GetComponent<FootPrintComponent>();

                var age = Math.Abs((footPrint.Foot.FootPrintCount - footPrint.Count) - footPrint.Foot.MaxFootPrintCount);
                float normalizedAge = Math.Min(age / (float)footPrint.Foot.MaxFootPrintCount, 1.0f);
                var opacity = (0f + normalizedAge);

                footPrintEntity.Sprite.Color = Color.White * opacity;
                footPrint.FollowChance = Math.Clamp(opacity, 0.25f, 1f); // perhaps follow chance should start at 25-50% then increase from there

                if (footPrint.Foot.FootPrintCount - footPrint.Count >= footPrint.Foot.MaxFootPrintCount)
                {
                    footPrintToRemove = footPrintEntity;
                }
            });

            if (footPrintToRemove != null)
            {
                EntityHandler.Remove(footPrintToRemove);
            }
        }
    }
}
