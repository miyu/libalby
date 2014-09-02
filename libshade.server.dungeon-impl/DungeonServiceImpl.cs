using Shade.Server.LevelHostManager;

namespace Shade.Server.Dungeons
{
    public class DungeonServiceImpl : DungeonService
    {
       private readonly DynamicLevelHostManagerService dynamicLevelHostManagerService;

       public DungeonServiceImpl(ShadeServiceLocator shadeServiceLocator, DynamicLevelHostManagerService dynamicLevelHostManagerService)
       {
          shadeServiceLocator.RegisterService(typeof(DungeonService), this);

          this.dynamicLevelHostManagerService = dynamicLevelHostManagerService;
       }

       public Dungeon CreateDebugDungeon()
       {
          //dynamicLevelHostManagerService
          return null;
       }
    }
}
