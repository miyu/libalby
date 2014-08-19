using System;
using SharpDX.Toolkit;

namespace Shade.Helios
{
   public interface IEngineGame : IDisposable
   {
      void Run(GameContext context = null);

      event EngineInitializeHandler Initialize;
      event EngineUpdateHandler Update;

      GameWindow Window { get; }
      GameServiceRegistry Services { get; }
   }

   public delegate void EngineInitializeHandler();
   public delegate void EngineUpdateHandler(GameTime gameTime);
}