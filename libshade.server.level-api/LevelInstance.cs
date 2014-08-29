using Shade.Server.Nierians;
using Shade.Server.Nierians.DTOs;

namespace Shade.Server.Level
{
   public interface LevelInstance
   {
      // void Enter(NierianIdV1 nierianKey);
      // void Leave(NierianIdV1 nierianKey);

      bool IsTransient { get; }
   }
}
