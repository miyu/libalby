using System.Collections.Generic;
using ItzWarty.Services;
using Shade.Server.Accounts;
using Shade.Server.Dungeons;
using Shade.Server.Level;
using Shade.Server.LevelHostManager;
using Shade.Server.Nierians;
using Shade.Server.SpecializedCache;
using Shade.Server.World;

namespace Shade.Server
{
   public interface ShadeServiceLocator : IServiceLocator
   {
      PlatformConfiguration Configuration { get; }

      PlatformCacheService PlatformCacheService { get; }
      SpecializedCacheService SpecializedCacheService { get; }

      AccountService AccountService { get; }
      NierianService NierianService { get; }

      DynamicLevelHostManagerService DynamicLevelHostManagerService { get; }
      WorldLevelHostManagerService WorldLevelHostManagerService { get; }

      DungeonService DungeonService { get; }

      IReadOnlyCollection<WorldService> WorldServices { get; }
   }
}
