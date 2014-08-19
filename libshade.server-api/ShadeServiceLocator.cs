using ItzWarty.Services;
using libshade.server;
using Shade.Server.Accounts;
using Shade.Server.Dungeon;
using Shade.Server.Nierians;

namespace Shade.Server
{
   public interface ShadeServiceLocator : IServiceLocator
   {
      PlatformConfiguration Configuration { get; }

      LevelService LevelService { get; }
      DungeonService DungeonService { get; }
      AccountService AccountService { get; }
      NierianService NierianService { get; }
   }
}
