using SharpDX;
using SharpDX.Toolkit;
using System;

namespace Shade.Helios
{
   public class Engine : IDisposable
   {
      private readonly IEngineGame game;
      private SceneManager sceneManager;

      public Engine()
      {
         this.game = new EngineGame(this);
         this.game.Initialize += HandleGameInitialize;
         this.game.Update += HandleGameUpdate;

         this.sceneManager = new SceneManager(this);
      }

      public void Run(GameContext context = null) { this.game.Run(context); }
      public void SetTitle(string title) { this.game.Window.Title = title; }

      protected virtual void HandleGameInitialize()
      {
      }

      protected virtual void HandleGameUpdate(GameTime gameTime)
      {
      }

      public void Dispose() { this.game.Dispose(); }

      public IServiceRegistry Services { get { return this.game.Services; } }
   }
}