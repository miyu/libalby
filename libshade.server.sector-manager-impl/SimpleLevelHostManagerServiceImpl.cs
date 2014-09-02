using System;
using Shade.Server.Level;
using Shade.Server.World.LevelHost;

namespace Shade.Server.LevelHostManager
{
    public class SimpleLevelHostManagerServiceImpl : DynamicLevelHostManagerService, WorldLevelHostManagerService
    {
       private readonly LevelHostService levelHostService;

       public SimpleLevelHostManagerServiceImpl()
       {
          levelHostService = new LevelHostServiceImpl();
       }

       public LevelHostService LookupLevelHostService(ulong levelId)
       {
          return levelHostService;
       }
    }
}
