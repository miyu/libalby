using System;
using Shade.Server.Level;

namespace Shade.Server.World.LevelHost
{
   public interface LevelHostService
   {
      LevelInstance GetLevel(ulong levelId);
   }
}
