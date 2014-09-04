using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using ItzWarty;
using ItzWarty.Geometry;
using Poly2Tri;
using Shade.Entities;
using Shade.Helios.Assets;
using Shade.Helios.Entities;
using Shade.Helios.State;
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
      private Effect debugEffect;
      private PrimitiveBatch<VertexPositionColor> debugBatch;

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
         debugBatch = new PrimitiveBatch<VertexPositionColor>(GraphicsDevice);

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

         Console.WriteLine("Load Debug Effect");
         var d = new EffectCompiler().CompileFromFile("Shaders/DebugSolid.hlsl", EffectCompilerFlags.Debug);
         Console.WriteLine("SUCCESS? " + !d.HasErrors);
         foreach (var message in d.Logger.Messages) {
            Console.WriteLine(message.Text);
         }
         foreach (var shader in d.EffectData.Shaders) {
            Console.WriteLine("HAVE SHADER " + shader.Name);
         }
         debugEffect = new Effect(GraphicsDevice, d.EffectData, GraphicsDevice.DefaultEffectPool);
         foreach (var x in debugEffect.Parameters) {
            Console.WriteLine("PARAMETER " + x.Name);
         }
      }

      protected virtual void HandleGameUpdate(GameTime gameTime)
      {
         SceneManager.ActiveScene.Update(gameTime);
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

            var camera = scene.GetCamera();
            var cameraComponent = (ICameraComponent)camera.GetComponentOrNull(ComponentType.Camera);

            foreach (var entity in scene.EnumerateEntities()) {
               var model = (ModelComponent)entity.GetComponentOrNull(ComponentType.Model);
               var transform = (ITransformComponent)entity.GetComponentOrNull(ComponentType.Transform);

               if (model == null || transform == null) {
                  continue;
               }

               var diffuseTexture = AssetService.GetAsset<Texture2D>(model.DiffuseTexture);
               var mesh = AssetService.GetAsset<Mesh>(model.Mesh);

               basicEffect.LightingEnabled = true;
               basicEffect.Texture = diffuseTexture;
               basicEffect.World = mesh.ModelTransform * transform.WorldTransform;   
               basicEffect.View = cameraComponent.View;
               basicEffect.Projection = cameraComponent.Projection;

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
            }

            GraphicsDevice.SetBlendState(GraphicsDevice.BlendStates.AlphaBlend);
            GraphicsDevice.SetRasterizerState(wireframeRasterizerState);
            debugEffect.DefaultParameters.WorldParameter.SetValue(Matrix.Identity);
            debugEffect.DefaultParameters.ViewParameter.SetValue(cameraComponent.View);
            debugEffect.DefaultParameters.ProjectionParameter.SetValue(cameraComponent.Projection);
            debugEffect.CurrentTechnique.Passes[0].Apply();
            debugBatch.Begin();

            foreach (var entity in SceneManager.ActiveScene.EnumerateEntities()) {
               var pathing = entity.GetComponentOrNull<IPathingComponent>(ComponentType.Pathing);
               if (pathing != null && pathing.Route != null) {
                  var route = pathing.Route;
                  var path = route.Path;
                  Func<Vector3, Vector3> bias = v => new Vector3(v.X, 0.01f, v.Z);
                  debugBatch.DrawLine(new VertexPositionColor(bias(route.StartPoint), Color.Cyan), new VertexPositionColor(bias(path[0]), Color.Cyan));
                  for (var i = 0; i < path.Count - 1; i++) {
                     debugBatch.DrawLine(new VertexPositionColor(bias(path[i]), Color.Cyan), new VertexPositionColor(bias(path[i + 1]), Color.Cyan));
                  }
               }
            }

//            var navmesh = SceneManager.ActiveScene.GetNavigationMesh();
//            foreach (var node in navmesh.EnumerateNodes()) {
//               var vertices = node.Vertices;
//               Func<Point3D, Vector3> bias = v => new Vector3((float)v.X, (float)(v.Y + 0.01f), (float)v.Z);
//               for (var i = 0; i < vertices.Length; i++) {
//                  debugBatch.DrawLine(new VertexPositionColor(bias(vertices[i]), Color.Cyan), new VertexPositionColor(bias(vertices[(i + 1) % vertices.Length]), Color.Cyan));
//               }
//            }

            var polygon = new Polygon(new List<PolygonPoint> { new PolygonPoint(-5, -5), new PolygonPoint(-5,-2), new PolygonPoint(-5, 2), new PolygonPoint(-5, 5), new PolygonPoint(-2, 5), new PolygonPoint(2, 5), new PolygonPoint(5, 5), new PolygonPoint(5, 2), new PolygonPoint(5,-2), new PolygonPoint(5, -5), new PolygonPoint(2, -5), new PolygonPoint(-2, -5) });
            polygon.AddHole(new Polygon(new List<PolygonPoint> { new PolygonPoint(2.2f, 1.5f), new PolygonPoint(3.5f, 0.8f), new PolygonPoint(3.9f, 1.6f), new PolygonPoint(2.7f, 2.3f) }));
            P2T.Triangulate(polygon);
            foreach (var triangle in polygon.Triangles) {
               var triangleCentroid = triangle.Centroid();
               var points = triangle.Points;
               float bias = 0.01f;
               var fillColor = Color.Premultiply(new Color(0.0f, 1.0f, 1.0f, 0.8f));
               debugBatch.DrawTriangle(
                  new VertexPositionColor(new Vector3(points._0.Xf, bias, points._0.Yf), fillColor),
                  new VertexPositionColor(new Vector3(points._1.Xf, bias, points._1.Yf), fillColor),
                  new VertexPositionColor(new Vector3(points._2.Xf, bias, points._2.Yf), fillColor) 
               );

               foreach (var neighbor in triangle.Neighbors.Where((n) => n != null && n.IsInterior))
               {
                  var neighborCentroid = neighbor.Centroid();
                  debugBatch.DrawLine(
                     new VertexPositionColor(new Vector3(triangleCentroid.Xf, bias, triangleCentroid.Yf), Color.Red),
                     new VertexPositionColor(new Vector3(neighborCentroid.Xf, bias, neighborCentroid.Yf), Color.Red) 
                  );
               }
            }

            debugBatch.End();
            /*
               //var box = mesh.BoundingBox;
               //var corners = box.GetCorners();
               //primitives.DrawTriangle(new VertexPositionColor(new Vector3(0, 0, 0), Color.White), new VertexPositionColor(new Vector3(1, 0, 0), Color.White), new VertexPositionColor(new Vector3(1, 0, 1), Color.White));
               //primitives.DrawLine(new VertexPositionColor(new Vector3(0, 0, 0), Color.Cyan), new VertexPositionColor(new Vector3(0, 0, 0), Color.Cyan));
               //primitives.DrawLine(new VertexPositionColor(corners[1], Color.White), new VertexPositionColor(corners[2], Color.White));
               //primitives.DrawLine(new VertexPositionColor(corners[2], Color.Lime), new VertexPositionColor(corners[3], Color.White));
               //primitives.DrawLine(new VertexPositionColor(corners[3], Color.White), new VertexPositionColor(corners[0], Color.White));
             */

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