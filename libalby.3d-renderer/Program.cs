using System;
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
         var heroTexture = TextureLoader.LoadTexture("lime.jpg");
         var enemyTexture = TextureLoader.LoadTexture("red.jpg");
         var floorTexture = TextureLoader.LoadTexture("arroway.de_tiles-68_s100-g100-r100.jpg");
         var crateTexture = TextureLoader.LoadTexture("crate0/crate0_diffuse.png");

         var floorBlockMesh = MeshLoader.TransformMesh(MeshLoader.UnitCubeMesh, Matrix.Translation(0, 0.5f, 0));
         var heroMesh = MeshLoader.TransformMesh(floorBlockMesh, Matrix.Scaling(0.7f, 1.3f, 0.7f));
         var enemyMesh = MeshLoader.TransformMesh(floorBlockMesh, Matrix.Scaling(0.5f, 0.3f, 0.5f));
         var floorMesh = MeshLoader.TransformMesh(MeshLoader.UnitCubeMesh, Matrix.Translation(0, -0.5f, 0) * Matrix.Scaling(10.0f, 0.5f, 10.0f));
         var crateMesh = MeshLoader.TransformMesh(floorBlockMesh, Matrix.Scaling(0.9f, 0.65f, 0.65f));

         var scene = new Scene();
         heroEntity = new Entity();
         heroEntity.AddComponent(new RenderComponent(heroTexture, heroMesh, Matrix.RotationY(10f * (float)Math.PI / 180f)));
         scene.AddEntity(heroEntity);

         const int enemyCount = 3;
         for (var i = 0; i < enemyCount; i++) {
            var enemyEntity = new Entity();
            enemyEntity.AddComponent(new RenderComponent(enemyTexture, enemyMesh, Matrix.Translation(0, 0, 4.0f) * Matrix.RotationY(MathUtil.DegreesToRadians(20.0f + (360.0f * i) / enemyCount))));
            scene.AddEntity(enemyEntity);
         }

         var floorEntity = new Entity();
         floorEntity.AddComponent(new RenderComponent(floorTexture, floorMesh, Matrix.Translation(0, 0, 0)));
         scene.AddEntity(floorEntity);

         var crateEntity = new Entity();
         crateEntity.AddComponent(new RenderComponent(crateTexture, crateMesh, Matrix.RotationY(30.0f * (float)Math.PI / 180f) * Matrix.Translation(3, 0, 1.6f)));
         scene.AddEntity(crateEntity);

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
         
         //Console.WriteLine(Mouse.X + " " + Mouse.Y);
         var ray = Ray.GetPickRay(Mouse.X, Mouse.Y, this.GraphicsDevice.Viewport, CameraService.View * CameraService.Projection);
         var projection = plane.ProjectRayToSurface(new Ray3D(new Point3D(ray.Position.X, ray.Position.Y, ray.Position.Z), new Vector3D(ray.Direction.X, ray.Direction.Y, ray.Direction.Z)));
         Console.WriteLine(projection);
//         projection = plane.PlanarToWorld(new Point2D(1, 0));

         var r = (RenderComponent)heroEntity.GetComponentOrNull(ComponentType.Renderable);
//         r.WorldTransform = Matrix.Translation(ray.Position + ray.Direction * 10);
         r.WorldTransform = Matrix.Translation((float)projection.X, (float)projection.Y, (float)projection.Z);
         Console.WriteLine(ray.Position + " " + ray.Direction);
         if (Keyboard.IsKeyDown(Keys.Left)) {
            //SceneManager.ActiveScene.EnumerateEntities().First();
            //CameraService.NudgeLeft(0);
         }
      }

      public IServiceRegistry Services { get { return base.Services; } }
   }
}
