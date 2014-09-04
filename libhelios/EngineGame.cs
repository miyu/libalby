using System;
using System.Data;
using Shade.Helios.Assets;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;
using SharpDX.Toolkit.Input;

namespace Shade.Helios
{
   public class EngineGame : Game, IEngineGame
   {
      private readonly Engine engine;
      private readonly GraphicsDeviceManager graphicsDeviceManager;
      private readonly KeyboardManager keyboardManager;
      private readonly MouseSubsystem mouseSubsystem;
      private readonly SceneRenderer sceneRenderer;
      private readonly CameraProvider cameraProvider;
      private readonly AssetService assetService;
      private readonly TextureLoader textureLoader;
      private readonly MeshLoaderService meshLoader;

      private KeyboardState keyboardState;
      
      public EngineGame(Engine engine, string contentPath = "Content")
      {
         this.engine = engine;

         this.graphicsDeviceManager = new GraphicsDeviceManager(this);
         this.graphicsDeviceManager.PreferredBackBufferWidth = 1600;
         this.graphicsDeviceManager.PreferredBackBufferHeight = 900;
         this.graphicsDeviceManager.DeviceCreationFlags |= DeviceCreationFlags.Debug;
         this.keyboardManager = new KeyboardManager(this);
         this.mouseSubsystem = new MouseSubsystem(this, graphicsDeviceManager);
         this.sceneRenderer = new SceneRenderer(this);
         this.cameraProvider = new CameraProvider(this);
         this.assetService = new AssetService(this);
         this.textureLoader = new TextureLoader(this, assetService);
         this.meshLoader = new MeshLoaderService(this, assetService);

         Content.RootDirectory = contentPath;
      }

      public IGraphicsDeviceManager GraphicsDeviceManager { get { return graphicsDeviceManager; } }
      public KeyboardState KeyboardState { get { return keyboardState; } }
      public MouseSubsystem MouseSubsystem { get { return mouseSubsystem; } }
      public ICameraService CameraService { get { return cameraProvider; } }
      public IAssetService AssetService { get { return assetService; } }
      public ITextureLoaderService TextureLoader { get { return textureLoader; } }
      public IMeshLoaderService MeshLoader { get { return meshLoader; } }
      public new GraphicsDevice GraphicsDevice { get { return base.GraphicsDevice; } }

      private event EngineInitializeHandler _Initialize;  
      event EngineInitializeHandler IEngineGame.Initialize { add { _Initialize += value; } remove { _Initialize -= value; } }

      protected override void Initialize()
      {
         base.Initialize();

         this.Window.IsMouseVisible = true;
      }

      protected override void BeginRun()
      {
         base.BeginRun();

         Console.WriteLine("EngineGame.BeginRun - Invoke Initialize Event");
         var capture = _Initialize;
         if (capture != null)
            capture.Invoke();

         Console.WriteLine("EngineGame.BeginRun - Exit");
      }

      private event EngineUpdateHandler _Update;
      event EngineUpdateHandler IEngineGame.Update { add { _Update += value; } remove { _Update -= value; } }

      protected override void Update(GameTime gameTime)
      {
         base.Update(gameTime);

         keyboardState = keyboardManager.GetState();

         var capture = _Update;
         if (capture != null)
            capture.Invoke(gameTime);
      }

      private event EngineDrawHandler _Draw;
      event EngineDrawHandler IEngineGame.Draw { add { _Draw += value; } remove { _Draw -= value; } }

      protected override void Draw(GameTime gameTime)
      {
         base.Draw(gameTime);

         var capture = _Draw;
         if (capture != null)
            capture.Invoke(gameTime);
      }
   }
}
