using System.Collections.Generic;

namespace Shade.Alby
{
   public class Dungeon
   {

   }

   public interface ISector
   {
      ISector Parent { get; }
      IReadOnlyList<ISector> Peers { get; }
      IReadOnlyList<ISector> Body { get; }
   }
}
