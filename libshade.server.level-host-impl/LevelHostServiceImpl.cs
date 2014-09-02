using ItzWarty;
using Shade.Server.Level;
using System.Collections.Generic;

namespace Shade.Server.World.LevelHost
{
    public class LevelHostServiceImpl : LevelHostService
    {
       private readonly Dictionary<ulong, LevelInstance> levelInstancesByLevelId = new Dictionary<ulong, LevelInstance>();

       public LevelHostServiceImpl() { }

       public LevelInstance GetLevel(ulong id) { return levelInstancesByLevelId.GetValueOrDefault(id); }
    }
}
