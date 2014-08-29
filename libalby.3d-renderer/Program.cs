using System;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Windows.Forms;
using NLog;
using Shade.Helios;
using Shade.Helios.State;
using Shade.Server;
using Shade.Server.Accounts;
using Shade.Server.Dungeon;
using Shade.Server.Level;
using Shade.Server.Nierians;
using Shade.Server.World;
using SharpDX;

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
      private readonly LevelInstance levelInstance;
      private readonly DungeonService dungeonService;

      private readonly ClientLevelService clientLevelService;
      private readonly GameControllerService gameControllerService;

      public Client()
      {
         // Server-side stuff - Don't attach to client-side logic!
         var shardConfiguration = new ShardConfiguration("LOCAL");
         var platformConfiguration = new PlatformConfiguration(new[] { shardConfiguration });
         shadeServiceLocator = new ShadeServiceLocatorImpl(platformConfiguration);
         accountService = shadeServiceLocator.AccountService;
         nierianService = shadeServiceLocator.NierianService;
         worldService = shadeServiceLocator.WorldServices.First();
         levelInstance = shadeServiceLocator.LevelInstance;
         dungeonService = shadeServiceLocator.DungeonService;

         // Client-side stuff
         clientLevelService = new ClientLevelServiceImpl(this);
         gameControllerService = new GameControllerServiceImpl(this, clientLevelService);
      }

      protected override void HandleGameInitialize()
      {
         // initialize server stuff
         var accountKey = accountService.CreateAccount("LOCAL", "ItzWarty");
         var nierianKey = nierianService.CreateNierian(accountKey, "Warty");
         var nierians = nierianService.EnumerateNieriansByAccount(accountKey);

         // initialize client stuff
         base.HandleGameInitialize();

         // var loginResult = worldService.Enter(nierianKey);s
         SetTitle("Engine");
      }

      public IServiceRegistry Services { get { return base.Services; } }
   }
}
