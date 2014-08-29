using System;
using Shade.Server.Level;

namespace Shade.Server.Dungeon
{
    public class DungeonServiceImpl : DungeonService
    {
       private readonly LevelInstance levelInstance;

       public DungeonServiceImpl(ShadeServiceLocator shadeServiceLocator, LevelInstance levelInstance)
       {
          this.levelInstance = levelInstance;

          shadeServiceLocator.RegisterService(typeof(DungeonService), this);
       }

       public Dungeon CreateDebugDungeon()
       {
          throw new NotImplementedException();
       }
    }
}
