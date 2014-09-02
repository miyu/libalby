using System;
using System.Collections.Generic;
using ItzWarty.Services;
using Shade.Server.Accounts;
using Shade.Server.Cache;
using Shade.Server.Dungeons;
using Shade.Server.Level;
using Shade.Server.LevelHostManager;
using Shade.Server.Nierians;
using Shade.Server.SpecializedCache;
using Shade.Server.World;

namespace Shade.Server
{
   public class ShadeServiceLocatorImpl : ShadeServiceLocator
   {
      private readonly ServiceLocator serviceLocator = new ServiceLocator();
      private readonly PlatformConfiguration configuration;
      private readonly PlatformCacheService platformCacheService;
      private readonly SpecializedCacheService specializedCacheService;
      private readonly AccountService accountService;
      private readonly NierianService nierianService;

      private readonly SimpleLevelHostManagerServiceImpl levelHostManagerService;

      private readonly List<WorldService> worldServices = new List<WorldService>();
      private readonly DungeonService dungeonService;

      public ShadeServiceLocatorImpl(PlatformConfiguration configuration)
      {
         this.configuration = configuration;

         platformCacheService = new CacheServiceImpl(this, configuration);
         specializedCacheService = new SpecializedCacheServiceImpl(this, configuration, platformCacheService);

         accountService = new AccountServiceImpl(this, configuration, platformCacheService, specializedCacheService);
         nierianService = new NierianServiceImpl(this, configuration, platformCacheService, specializedCacheService);

         levelHostManagerService = new SimpleLevelHostManagerServiceImpl();

         dungeonService = new DungeonServiceImpl(this, levelHostManagerService);
         
         worldServices.Add(new WorldServiceImpl(this, configuration, platformCacheService, nierianService, dungeonService));
      }

      public PlatformConfiguration Configuration { get; private set; }
      
      public PlatformCacheService PlatformCacheService { get { return platformCacheService; } }
      public SpecializedCacheService SpecializedCacheService { get { return specializedCacheService; } }

      public AccountService AccountService { get { return accountService; } }
      public NierianService NierianService { get { return nierianService; } }

      public DynamicLevelHostManagerService DynamicLevelHostManagerService { get { return levelHostManagerService; } }
      public WorldLevelHostManagerService WorldLevelHostManagerService { get { return levelHostManagerService; } }

      public DungeonService DungeonService { get { return dungeonService; } }

      public IReadOnlyCollection<WorldService> WorldServices { get { return worldServices; } }

      public void RegisterService(Type type, object provider)
      {
         serviceLocator.RegisterService(type, provider);
      }
      public T GetService<T>() { return serviceLocator.GetService<T>(); }
   }
}
