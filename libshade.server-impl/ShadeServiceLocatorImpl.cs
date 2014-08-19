using System;
using ItzWarty.Services;
using libshade.server;
using Shade.Server.Accounts;
using Shade.Server.Dungeon;
using Shade.Server.Nierians;
using Shade.Server.World;

namespace Shade.Server
{
   public class ShadeServiceLocatorImpl : ShadeServiceLocator
   {
      private readonly ServiceLocator serviceLocator = new ServiceLocator();
      private readonly PlatformConfiguration configuration;
      private readonly AccountService accountService;
      private readonly NierianService nierianService;
      private readonly LevelService levelService;
      private readonly DungeonService dungeonService;

      public ShadeServiceLocatorImpl(PlatformConfiguration configuration)
      {
         this.configuration = configuration;

         accountService = new AccountServiceImpl(this, configuration);
         nierianService = new NierianServiceImpl(this, configuration);
         levelService = new LevelServiceImpl(this);
         dungeonService = new DungeonServiceImpl(this, levelService);
      }

      public PlatformConfiguration Configuration { get; private set; }

      public AccountService AccountService { get { return accountService; } }
      public NierianService NierianService { get { return nierianService; } }
      public LevelService LevelService { get { return levelService; } }
      public DungeonService DungeonService { get { return dungeonService; } }

      public void RegisterService(Type type, object provider)
      {
         serviceLocator.RegisterService(type, provider);
      }
      public T GetService<T>() { return serviceLocator.GetService<T>(); }
   }
}
