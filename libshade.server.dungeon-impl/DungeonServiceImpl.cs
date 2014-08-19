using System;
using libshade.server;

namespace Shade.Server.Dungeon
{
    public class DungeonServiceImpl : DungeonService
    {
       private readonly LevelService levelService;

       public DungeonServiceImpl(ShadeServiceLocator shadeServiceLocator, LevelService levelService)
       {
          this.levelService = levelService;

          shadeServiceLocator.RegisterService(typeof(DungeonService), this);
       }

       public Dungeon CreateDebugDungeon()
       {
          throw new NotImplementedException();
       }
    }
}
