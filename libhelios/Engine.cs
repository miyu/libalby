using System.Runtime.InteropServices;
using ItzWarty;
using Shade.Entities;
using Shade.Helios.Assets;
using Shade.Helios.Entities;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.Toolkit;
using System;
using SharpDX.Toolkit.Graphics;
using SharpDX.Toolkit.Input;
using RasterizerState = SharpDX.Toolkit.Graphics.RasterizerState;
using Texture2D = SharpDX.Toolkit.Graphics.Texture2D;

namespace Shade.Helios
{
   public class Engine : IDisposable
   {
      private readonly IEngineGame game;
      private readonly SceneManager sceneManager;

      private BasicEffect basicEffect;
      private BasicEffect wireframeEffect;
      private RasterizerState wireframeRasterizerState;

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
      public MouseSubsystem Mouse { get { return game.MouseSubsystem; } }
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
         var wireframeRasterizerStateDescription = RasterizerStateDescription.Default();
         wireframeRasterizerStateDescription.FillMode = FillMode.Wireframe;
         wireframeRasterizerStateDescription.CullMode = CullMode.None;
         wireframeRasterizerStateDescription.DepthBias = -500;

         wireframeRasterizerState = RasterizerState.New(GraphicsDevice, wireframeRasterizerStateDescription);
         
         basicEffect = new BasicEffect(GraphicsDevice);
         basicEffect.EnableDefaultLighting(); // enable default lightning, useful for quick prototyping
         basicEffect.TextureEnabled = true;   // enable texture drawing

         wireframeEffect = new BasicEffect(GraphicsDevice);
         wireframeEffect.TextureEnabled = true;
         wireframeEffect.Texture = AssetService.GetAsset<Texture2D>(TextureLoader.WhiteTextureHandle);
      }

      protected virtual void HandleGameUpdate(GameTime gameTime)
      {
      }

      protected virtual void HandleGameDraw(GameTime gameTime)
      {
         var scene = SceneManager.ActiveScene;
         if (scene == null) {
            GraphicsDevice.Clear(Color4.Black);
         }
         else
         {
            GraphicsDevice.Clear(new Color4(new Color3(0x303030)));

            foreach (var entity in scene.EnumerateEntities()) {
               var model = (ModelComponent)entity.GetComponentOrNull(ComponentType.Model);
               var transform = (ITransformComponent)entity.GetComponentOrNull(ComponentType.Transform);
               var diffuseTexture = AssetService.GetAsset<Texture2D>(model.DiffuseTexture);
               var mesh = AssetService.GetAsset<Mesh>(model.Mesh);

               basicEffect.LightingEnabled = true;
               basicEffect.Texture = diffuseTexture;
               basicEffect.World = mesh.ModelTransform * transform.WorldTransform;
               basicEffect.View = CameraService.View;
               basicEffect.Projection = CameraService.Projection;

               wireframeEffect.World = basicEffect.World;
               wireframeEffect.View = basicEffect.View;
               wireframeEffect.Projection = basicEffect.Projection;

               var passes = basicEffect.CurrentTechnique.Passes;
               foreach (var pass in passes) {
                  pass.Apply();

                  GraphicsDevice.SetRasterizerState(GraphicsDevice.RasterizerStates.Default);
                  GraphicsDevice.SetVertexBuffer(0, mesh.VertexBuffer, 0);
                  GraphicsDevice.SetVertexInputLayout(mesh.InputLayout);
                  GraphicsDevice.SetIndexBuffer(mesh.IndexBuffer, mesh.IsIndex32Bits, 0);
                  GraphicsDevice.DrawIndexed(PrimitiveType.TriangleList, mesh.IndexBuffer.ElementCount, 0, 0);
               }

               var pickable = (PickableComponent)entity.GetComponentOrNull(ComponentType.Pickable);
               if (pickable != null) {
                  var boxMesh = AssetService.GetAsset<Mesh>(MeshLoader.UnitCubeMesh);
                  wireframeEffect.World = pickable.BoundingBox.Transformation * transform.WorldTransform;
                  passes = wireframeEffect.CurrentTechnique.Passes;
                  foreach (var pass in passes) {
                     pass.Apply();

                     GraphicsDevice.SetRasterizerState(wireframeRasterizerState);
                     GraphicsDevice.SetVertexBuffer(0, boxMesh.VertexBuffer, 0);
                     GraphicsDevice.SetVertexInputLayout(boxMesh.InputLayout);
                     GraphicsDevice.SetIndexBuffer(boxMesh.IndexBuffer, boxMesh.IsIndex32Bits, 0);
                     GraphicsDevice.DrawIndexed(PrimitiveType.TriangleList, mesh.IndexBuffer.ElementCount, 0, 0);
                  }
               }

               //var box = mesh.BoundingBox;
               //var corners = box.GetCorners();
               //primitives.DrawTriangle(new VertexPositionColor(new Vector3(0, 0, 0), Color.White), new VertexPositionColor(new Vector3(1, 0, 0), Color.White), new VertexPositionColor(new Vector3(1, 0, 1), Color.White));
               //primitives.DrawLine(new VertexPositionColor(new Vector3(0, 0, 0), Color.Cyan), new VertexPositionColor(new Vector3(0, 0, 0), Color.Cyan));
               //primitives.DrawLine(new VertexPositionColor(corners[1], Color.White), new VertexPositionColor(corners[2], Color.White));
               //primitives.DrawLine(new VertexPositionColor(corners[2], Color.Lime), new VertexPositionColor(corners[3], Color.White));
               //primitives.DrawLine(new VertexPositionColor(corners[3], Color.White), new VertexPositionColor(corners[0], Color.White));
            }

            //basicEffect.Texture = AssetService.GetAsset<Texture2D>(TextureLoader.WhiteTextureHandle);
            //basicEffect.LightingEnabled = false;
            //pass.Apply();
            //var rasterizerDesc = RasterizerStateDescription.Default();
            //rasterizerDesc.FillMode = FillMode.Wireframe;
            //rasterizerDesc.CullMode = CullMode.None;
            //rasterizerDesc.DepthBias = -500;
            //GraphicsDevice.SetRasterizerState(RasterizerState.New(GraphicsDevice, rasterizerDesc));
            //GraphicsDevice.DrawIndexed(PrimitiveType.TriangleList, mesh.IndexBuffer.ElementCount, 0, 0);
         }
      }

      public void Dispose() { this.game.Dispose(); }

      public IServiceRegistry Services { get { return this.game.Services; } }
   }
}