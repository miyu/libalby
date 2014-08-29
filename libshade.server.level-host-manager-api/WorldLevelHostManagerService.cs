using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shade.Server.Level;
using Shade.Server.World.LevelHost;

namespace Shade.Server.LevelHostManager
{
   public interface WorldLevelHostManagerService
   {
      LevelHostService LookupLevelHostService(ulong id);
   }
}
