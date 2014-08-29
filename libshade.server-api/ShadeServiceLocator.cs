using System.Collections.Generic;
using ItzWarty.Services;
using Shade.Server.Accounts;
using Shade.Server.Dungeon;
using Shade.Server.Level;
using Shade.Server.Nierians;
using Shade.Server.World;

namespace Shade.Server
{
   public interface ShadeServiceLocator : IServiceLocator
   {
      PlatformConfiguration Configuration { get; }

      PlatformCacheService PlatformCacheService { get; }

      LevelInstance LevelInstance { get; }
      DungeonService DungeonService { get; }
      AccountService AccountService { get; }
      NierianService NierianService { get; }

      IReadOnlyCollection<WorldService> WorldServices { get; }
   }
}
