using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shade.Server.Dungeon
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
