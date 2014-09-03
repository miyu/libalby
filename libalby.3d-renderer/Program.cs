using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Windows.Forms;
using ItzWarty.Geometry;
using NLog;
using Shade.Entities;
using Shade.Helios;
using Shade.Helios.Entities;
using Shade.Helios.Pathfinding;
using Shade.Helios.State;
using Shade.Server;
using Shade.Server.Accounts;
using Shade.Server.Dungeons;
using Shade.Server.Level;
using Shade.Server.Nierians;
using Shade.Server.World;
using SharpDX;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;
using SharpDX.Toolkit.Input;
using Keys = SharpDX.Toolkit.Input.Keys;

namespace Shade.Client
{
   class Program
   {
      private static void Main(string[] args)
      {
         Application.EnableVisualStyles();
         Console.WindowWidth = Console.BufferWidth = 200;

         using (var game = new Client()) {
            game.Run();
         }
      }
   }

   public class Client : Engine, IShadeClient
   {
      private static Logger logger = LogManager.GetCurrentClassLogger();

      private readonly ShadeServiceLocator shadeServiceLocator;
      private readonly AccountService accountService;
      private readonly NierianService nierianService;
      private readonly WorldService worldService;
      private readonly DungeonService dungeonService;

      private readonly ClientLevelService clientLevelService;
      private readonly GameControllerService gameControllerService;

      private const string SHARD_ID = "LOCAL";

      private Entity heroEntity;

      public Client()
      {
         // Server-side stuff - Don't attach to client-side logic!
         var shardConfiguration = new ShardConfiguration(SHARD_ID);
         var platformConfiguration = new PlatformConfiguration(new[] { shardConfiguration });
         shadeServiceLocator = new ShadeServiceLocatorImpl(platformConfiguration);
         accountService = shadeServiceLocator.AccountService;
         nierianService = shadeServiceLocator.NierianService;
         worldService = shadeServiceLocator.WorldServices.First();
         dungeonService = shadeServiceLocator.DungeonService;

         // Client-side stuff
         clientLevelService = new ClientLevelServiceImpl(this);
         gameControllerService = new GameControllerServiceImpl(this, clientLevelService);
      }

      protected override void HandleGameInitialize()
      {
         // initialize server stuff
         var accountKey = accountService.CreateAccount(SHARD_ID, "ItzWarty");
         var nierianKey = nierianService.CreateNierian(SHARD_ID, accountKey.AccountId, "Warty");
         var nierians = nierianService.EnumerateNieriansByAccount(SHARD_ID, accountKey.AccountId);

         foreach(var nierian in nierians)
            Console.WriteLine(nierian); 

         // initialize client stuff
         base.HandleGameInitialize();
         var blockTexture = TextureLoader.LoadTexture("logo_large.png");
         var heroTexture = TextureLoader.LimeTextureHandle;
         var enemyTexture = TextureLoader.RedTextureHandle;
         var floorTexture = TextureLoader.LoadTexture("arroway.de_tiles-68_s100-g100-r100.jpg");
         var crateTexture = TextureLoader.LoadTexture("crate0/crate0_diffuse.png");

         var floorBlockMesh = MeshLoader.TransformMesh(MeshLoader.UnitCubeMesh, Matrix.Translation(0, 0.5f, 0));
         var heroMesh = MeshLoader.TransformMesh(floorBlockMesh, Matrix.Scaling(0.7f, 1.3f, 0.7f));
         var enemyMesh = MeshLoader.TransformMesh(floorBlockMesh, Matrix.Scaling(0.5f, 0.3f, 0.5f));
         var floorMesh = MeshLoader.TransformMesh(MeshLoader.UnitCubeMesh, Matrix.Translation(0, -0.5f, 0) * Matrix.Scaling(10.0f, 0.5f, 10.0f));
         var crateMesh = MeshLoader.TransformMesh(floorBlockMesh, Matrix.Scaling(0.9f, 0.65f, 0.65f));

         var scene = new Scene();
         heroEntity = new Entity();
         heroEntity.AddComponent(new IdentifierComponent("Hero"));
         heroEntity.AddComponent(new ModelComponent(heroTexture, heroMesh));
         heroEntity.AddComponent(new PositionComponent(new Vector3(0, 0, 0)));
         heroEntity.AddComponent(new ScaleComponent(new Vector3(1, 1, 1)));
         heroEntity.AddComponent(new OrientationComponent(Quaternion.RotationAxis(new Vector3(0, 1, 0), MathUtil.DegreesToRadians(10.0f))));
         //heroEntity.AddComponent(new TransformComponent(Matrix.RotationY(10f * (float)Math.PI / 180f)));
         var hbb = AssetService.GetAsset<Mesh>(heroMesh).BoundingBox;
         hbb.Transform(Matrix.Scaling(1.1f));
         heroEntity.AddComponent(new PickableComponent(hbb));
         scene.AddEntity(heroEntity);

         const int enemyCount = 3;
         var enemyEntities = new List<Entity>();
         for (var i = 0; i < enemyCount; i++) {
            var enemyEntity = new Entity();
            enemyEntity.AddComponent(new IdentifierComponent("Enemy " + i));
            enemyEntity.AddComponent(new ModelComponent(enemyTexture, enemyMesh));
            enemyEntity.AddComponent(new TransformComponent(Matrix.Translation(0, 0, 4.0f) * Matrix.RotationY(MathUtil.DegreesToRadians(20.0f + (360.0f * i) / enemyCount))));
            var ebb = AssetService.GetAsset<Mesh>(enemyMesh).BoundingBox;
            ebb.Transform(Matrix.Scaling(1.2f));
            enemyEntity.AddComponent(new PickableComponent(ebb));
            scene.AddEntity(enemyEntity);
            enemyEntities.Add(enemyEntity);
         }

         var floorEntity = new Entity();
         floorEntity.AddComponent(new IdentifierComponent("Floor"));
         floorEntity.AddComponent(new ModelComponent(floorTexture, floorMesh));
         floorEntity.AddComponent(new TransformComponent());
         scene.AddEntity(floorEntity);

         var crateEntity = new Entity();
         crateEntity.AddComponent(new IdentifierComponent("Crate"));
         crateEntity.AddComponent(new ModelComponent(crateTexture, crateMesh));
         crateEntity.AddComponent(new TransformComponent(Matrix.RotationY(30.0f * (float)Math.PI / 180f) * Matrix.Translation(3, 0, 1.6f)));
         var cbb = AssetService.GetAsset<Mesh>(crateMesh).BoundingBox;
         cbb.Transform(Matrix.Scaling(1.2f));
         crateEntity.AddComponent(new PickableComponent(cbb));
         scene.AddEntity(crateEntity);

         var cameraEntity = new Entity();
         cameraEntity.AddComponent(new IdentifierComponent("Camera"));
         cameraEntity.AddComponent(new PositionComponent(new Vector3(0, 10f, 4f)));
         cameraEntity.AddComponent(new OrientationComponent(Quaternion.RotationAxis(Vector3.UnitX, -(float)Math.Atan(10.0f / 4.0f))));
         cameraEntity.AddComponent(new PositionedOrientedCameraComponent());
         scene.AddEntity(cameraEntity);
         scene.SetCamera(cameraEntity);

         SceneManager.AddScene(scene);
         SceneManager.ActiveScene = scene;

         //var loginResult = worldService.Enter(nierianKey.ShardId, nierianKey.AccountId, nierianKey.NierianId);
         SetTitle("Engine");
      }

      protected override void HandleGameUpdate(GameTime gameTime)
      {
         base.HandleGameUpdate(gameTime);

         var navmesh = new NavMesh();
         var plane = new ConvexPolygonNode(new[] { new Point3D(-5, 0, -5), new Point3D(5, 0, -5), new Point3D(5, 0, 5), new Point3D(-5, 0, 5) });

         var cameraComponent = (ICameraComponent)SceneManager.ActiveScene.GetCamera().GetComponentOrNull(ComponentType.Camera);
         var ray = Ray.GetPickRay(Mouse.X, Mouse.Y, this.GraphicsDevice.Viewport, cameraComponent.View * cameraComponent.Projection);
         var projection = plane.Raycast(new Ray3D(new Point3D(ray.Position.X, ray.Position.Y, ray.Position.Z), new Vector3D(ray.Direction.X, ray.Direction.Y, ray.Direction.Z)));
         if (Mouse.Left.Down && projection != null) {
            var r = (IPositionComponent)heroEntity.GetComponentOrNull(ComponentType.Position);
            r.Position = new Vector3((float)projection.X, (float)projection.Y, (float)projection.Z);
         }

         foreach (var entity in SceneManager.ActiveScene.EnumerateEntities()) {
            var identifier = (IdentifierComponent)entity.GetComponentOrNull(ComponentType.Identifier);
            var transform = (ITransformComponent)entity.GetComponentOrNull(ComponentType.Transform);
            var pickable = (PickableComponent)entity.GetComponentOrNull(ComponentType.Pickable);
            if (identifier != null && transform != null && pickable != null) {
               var worldBox = pickable.BoundingBox;
               worldBox.Transform(transform.WorldTransform);
               if (worldBox.Intersects(ref ray)) {
                  Console.WriteLine(identifier.Name);
               }
            }
         }
;
         var cameraOffset = new Vector3();
         if (Keyboard.IsKeyDown(Keys.Left))
            cameraOffset += Vector3.Left * (float)gameTime.ElapsedGameTime.TotalSeconds;
         if (Keyboard.IsKeyDown(Keys.Right))
            cameraOffset += Vector3.Right * (float)gameTime.ElapsedGameTime.TotalSeconds;
         if (Keyboard.IsKeyDown(Keys.Up))
            cameraOffset += Vector3.ForwardRH * (float)gameTime.ElapsedGameTime.TotalSeconds;
         if (Keyboard.IsKeyDown(Keys.Down))
            cameraOffset += Vector3.BackwardRH * (float)gameTime.ElapsedGameTime.TotalSeconds;
         
         var camera = SceneManager.ActiveScene.GetCamera();
         var cameraPosition = (IPositionComponent)camera.GetComponentOrNull(ComponentType.Position);
         cameraPosition.Position += cameraOffset;
      }

      public IServiceRegistry Services { get { return base.Services; } }
   }
}
