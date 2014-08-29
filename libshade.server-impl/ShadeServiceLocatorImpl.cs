using System;
using System.Collections.Generic;
using ItzWarty.Services;
using Shade.Server.Accounts;
using Shade.Server.Cache;
using Shade.Server.Dungeon;
using Shade.Server.Level;
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
      private readonly List<WorldService> worldServices = new List<WorldService>();
      private readonly LevelInstance levelInstance;
      private readonly DungeonService dungeonService;

      public ShadeServiceLocatorImpl(PlatformConfiguration configuration)
      {
         this.configuration = configuration;

         platformCacheService = new CacheServiceImpl(this, configuration);
         specializedCacheService = new SpecializedCacheServiceImpl(this, configuration, platformCacheService);

         accountService = new AccountServiceImpl(this, configuration, platformCacheService, specializedCacheService);
         nierianService = new NierianServiceImpl(this, configuration);

         levelInstance = new LevelInstanceImpl(this);
         dungeonService = new DungeonServiceImpl(this, levelInstance);
         worldServices.Add(new WorldServiceImpl(this, configuration));
      }

      public PlatformConfiguration Configuration { get; private set; }

      public PlatformCacheService PlatformCacheService { get { return platformCacheService; } }

      public AccountService AccountService { get { return accountService; } }
      public NierianService NierianService { get { return nierianService; } }
      public IReadOnlyCollection<WorldService> WorldServices { get { return worldServices; } }
      public LevelInstance LevelInstance { get { return levelInstance; } }
      public DungeonService DungeonService { get { return dungeonService; } }

      public void RegisterService(Type type, object provider)
      {
         serviceLocator.RegisterService(type, provider);
      }
      public T GetService<T>() { return serviceLocator.GetService<T>(); }
   }
}
