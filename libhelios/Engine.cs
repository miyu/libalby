using ItzWarty;
using Shade.Entities;
using Shade.Helios.Assets;
using Shade.Helios.Entities;
using SharpDX;
using SharpDX.Toolkit;
using System;
using SharpDX.Toolkit.Graphics;
using SharpDX.Toolkit.Input;

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
         this.game.Draw += HandleGameDraw;

         this.sceneManager = new SceneManager(this);
      }

      public IGraphicsDeviceManager GraphicsDeviceManager { get { return game.GraphicsDeviceManager; } }
      public KeyboardState Keyboard { get { return game.KeyboardState; } }
      public ICameraService CameraService { get { return game.CameraService; } }
      public IAssetService AssetService { get { return game.AssetService; } }
      public ITextureLoaderService TextureLoader { get { return game.TextureLoader; } }
      public IMeshLoaderService MeshLoader { get { return game.MeshLoader; } }
      public GraphicsDevice GraphicsDevice { get { return game.GraphicsDevice; } }
      public ISceneManager SceneManager { get { return sceneManager; } }

      public void Run(GameContext context = null) { this.game.Run(context); }
      public void SetTitle(string title) { this.game.Window.Title = title; }

      protected virtual void HandleGameInitialize()
      {
      }

      protected virtual void HandleGameUpdate(GameTime gameTime)
      {
      }

      private BasicEffect _basicEffect;

      protected virtual void HandleGameDraw(GameTime gameTime)
      {
         var scene = SceneManager.ActiveScene;
         if (scene == null) {
            GraphicsDevice.Clear(Color4.Black);
         }
         else
         {
            if (_basicEffect == null) {
               _basicEffect = new BasicEffect(GraphicsDevice);
               _basicEffect.EnableDefaultLighting(); // enable default lightning, useful for quick prototyping
               _basicEffect.TextureEnabled = true;   // enable texture drawing
            }

            GraphicsDevice.Clear(new Color4(new Color3(0x303030)));

            foreach (var entity in scene.EnumerateEntities()) {
               var component = (RenderComponent)entity.GetComponentOrNull(ComponentType.Renderable);
               var diffuseTexture = AssetService.GetAsset<Texture2D>(component.DiffuseTexture);
               var mesh = AssetService.GetAsset<Mesh>(component.Mesh);

               _basicEffect.Texture = diffuseTexture;
               _basicEffect.World = mesh.ModelTransform * component.WorldTransform;
               _basicEffect.View = CameraService.View;
               _basicEffect.Projection = CameraService.Projection;

               var passes = _basicEffect.CurrentTechnique.Passes;
               foreach (var pass in passes) {
                  pass.Apply();

                  GraphicsDevice.SetVertexBuffer(0, mesh.VertexBuffer, 0);
                  GraphicsDevice.SetVertexInputLayout(mesh.InputLayout);
                  GraphicsDevice.SetIndexBuffer(mesh.IndexBuffer, mesh.IsIndex32Bits, 0);
                  GraphicsDevice.DrawIndexed(PrimitiveType.TriangleList, mesh.IndexBuffer.ElementCount, 0, 0);  
               }
            }
         }
      }

      public void Dispose() { this.game.Dispose(); }

      public IServiceRegistry Services { get { return this.game.Services; } }
   }
}