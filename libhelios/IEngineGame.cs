using System;
using Shade.Helios.Assets;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;
using SharpDX.Toolkit.Input;

namespace Shade.Helios
{
   public interface IEngineGame : IDisposable
   {
      void Run(GameContext context = null);

      event EngineInitializeHandler Initialize;
      event EngineUpdateHandler Update;
      event EngineDrawHandler Draw;

      GameWindow Window { get; }

      GameServiceRegistry Services { get; }
      IGraphicsDeviceManager GraphicsDeviceManager { get; }
      KeyboardState KeyboardState { get; }
      ICameraService CameraService { get; }
      IAssetService AssetService { get; }
      ITextureLoaderService TextureLoader { get; }
      IMeshLoaderService MeshLoader { get; }
      GraphicsDevice GraphicsDevice { get; }
   }

   public delegate void EngineInitializeHandler();
   public delegate void EngineUpdateHandler(GameTime gameTime);
   public delegate void EngineDrawHandler(GameTime gameTime);
}