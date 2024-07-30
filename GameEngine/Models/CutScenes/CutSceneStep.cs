using GameEngine.Enums;
using GameEngine.Models.ECS.Core;
using System.Numerics;

namespace GameEngine.Models.CutScenes
{
    public class CutSceneStep
    {
        public int Step { get; set; }

        public CutSceneStepTypeEnum Type { get; set; }

        public Entity Character { get; set; }

        public string Text { get; set; }

        public Vector2? PositionGoal { get; set; }

        public CutSceneStep (int step, Entity character, string text)
        {
            Step = step;
            Type = CutSceneStepTypeEnum.Text;
            Character = character;
            Text = text;
        }

        public CutSceneStep(int step, Entity character, Vector2 positionGoal)
        {
            Step = step;
            Type = CutSceneStepTypeEnum.Move;
            Character = character;
            PositionGoal = positionGoal;
        }
    }
}
