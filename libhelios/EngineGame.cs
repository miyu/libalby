using System;
using System.Data;
using Shade.Helios.Assets;
using SharpDX;
using SharpDX.Toolkit;

namespace Shade.Helios
{
   public class EngineGame : Game, IEngineGame
   {
      private readonly Engine engine;
      private readonly GraphicsDeviceManager graphicsDeviceManager;
      private readonly SceneRenderer sceneRenderer;
      private readonly CameraProvider cameraProvider;
      private readonly AssetService assetService;
      private readonly TextureLoader textureLoader;

      public EngineGame(Engine engine, string contentPath = "Content")
      {
         this.engine = engine;

         this.graphicsDeviceManager = new GraphicsDeviceManager(this);
         this.sceneRenderer = new SceneRenderer(this);
         this.cameraProvider = new CameraProvider(this);
         this.assetService = new AssetService(this);
         this.textureLoader = new TextureLoader(this, assetService);

         Content.RootDirectory = contentPath;
      }

      private event EngineInitializeHandler _Initialize;  
      event EngineInitializeHandler IEngineGame.Initialize { add { _Initialize += value; } remove { _Initialize -= value; } }

      protected override void Initialize()
      {
         base.Initialize();

         this.Window.IsMouseVisible = true;

         var capture = _Initialize;
         if (capture != null)
            capture.Invoke();
      }

      private event EngineUpdateHandler _Update;
      event EngineUpdateHandler IEngineGame.Update { add { _Update += value; } remove { _Update -= value; } }

      protected override void Update(GameTime gameTime)
      {
         base.Update(gameTime);

         var capture = _Update;
         if (capture != null)
            capture.Invoke(gameTime);
      }

      protected override void Draw(GameTime gameTime)
      {
         base.Draw(gameTime);
         GraphicsDevice.Clear(Color4.White);
      }
   }
}
