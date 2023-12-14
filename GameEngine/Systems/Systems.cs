using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameEngine.Systems
{
    public class Systems
    {
        private readonly List<IInitializeSystem> _initializeSystems = new List<IInitializeSystem>();
        private readonly List<IUpdateSystem> _updateableSystems = new List<IUpdateSystem>();
        private readonly List<IDrawSystem> _drawableSystems = new List<IDrawSystem>();

        public Systems Add(ISystem system)
        {
            var initSystem = system as IInitializeSystem;
            var updateSystem = system as IUpdateSystem;
            var drawSystem = system as IDrawSystem;

            if (initSystem != null)
                _initializeSystems.Add(initSystem);

            if (updateSystem != null)
                _updateableSystems.Add(updateSystem);

            if (drawSystem != null)
                _drawableSystems.Add(drawSystem);

            if (initSystem == null && updateSystem == null)
            {
                // TODO: Throw Exception if no correct interface is implemented.
            }
            return this;
        }

        public void Initialize()
        {
            foreach (IInitializeSystem s in _initializeSystems)
                s.Initialize();
        }

        public void Update(GameTime gameTime)
        {
            foreach (IUpdateSystem s in _updateableSystems)
                s.Update(gameTime);
        }

        public void Draw()
        {
            foreach (IDrawSystem s in _drawableSystems)
                s.Draw();
        }
    }
}
