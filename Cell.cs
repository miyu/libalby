using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shade.Alby
{
   public class Cell
   {
      private readonly GridPosition position;

      public Cell(GridPosition position)
      {
         this.position = position;
      }

      public GridPosition Position { get { return position; } }
   }
}
