using System.Collections.Generic;

namespace Shade.Server.Dungeons
{
   public class Dungeon
   {
      private readonly ICollection<DungeonLevel> levels;

      public Dungeon(ICollection<DungeonLevel> levels) {
         this.levels = levels;
      }

      public ICollection<DungeonLevel> Levels { get { return Levels; } }
   }
}
