using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Input;
using SharpDX.Windows;

namespace Shade.Helios
{
   public class MouseSubsystem : GameSystem
   {
      private readonly GraphicsDeviceManager graphicsDeviceManager;
      private readonly MouseManager mouseManager;
      private MouseState mouseState;
      private int mouseX;
      private int mouseY;

      public MouseSubsystem(EngineGame game, GraphicsDeviceManager graphicsDeviceManager) 
         : base(game)
      {
         Enabled = true;

         this.graphicsDeviceManager = graphicsDeviceManager;
         this.mouseManager = new MouseManager(game);
         game.GameSystems.Add(this);
      }

      public override void Update(GameTime gameTime)
      {
         base.Update(gameTime);

         mouseState = mouseManager.GetState();
         mouseX = (int)(mouseState.X * graphicsDeviceManager.PreferredBackBufferWidth);
         mouseY = (int)(mouseState.Y * graphicsDeviceManager.PreferredBackBufferHeight);
      }

      public int X { get { return mouseX; } }
      public int Y { get { return mouseY; } }
   }
}
