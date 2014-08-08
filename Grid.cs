using System;
using System.Collections.Generic;
using System.Linq;
using ItzWarty;

namespace Shade.Alby
{
   public class SquareGrid
   {
      private readonly int width;
      private readonly int height;

      private readonly IList<SquareCell> cells = new List<SquareCell>();

      public SquareGrid(int width, int height)
      {
         this.width = width;
         this.height = height;

         for (var y = 0; y < this.height; y++) {
            for (var x = 0; x < this.width; x++) {
               var position = new GridPosition(x, y);
               var cell = new SquareCell(position);
               cells.Add(cell);
            }
         }
      }

      public IEnumerable<SquareCell> GetNeighboringCells(GridPosition position) { return GetNeighboringCellPositions(position).Select(Get); }

      public IEnumerable<GridPosition> GetNeighboringCellPositions(GridPosition position)
      {
         return GetNeighboringCellPositions(position.X, position.Y);
      }

      private IEnumerable<GridPosition> GetNeighboringCellPositions(int x, int y)
      {
         if (y != 0)
            yield return new GridPosition(x, y - 1);

         if (x != 0)
            yield return new GridPosition(x - 1, y);

         if (x != this.width - 1)
            yield return new GridPosition(x + 1, y);

         if (y != this.height - 1)
            yield return new GridPosition(x, y + 1);
      }

      public SquareCell Get(GridPosition position) { return Get(position.X, position.Y); }

      public SquareCell Get(int x, int y) { return this.cells[y * width + x]; }

      public bool IsValidCell(int x, int y) { return x.WithinIE(0, width) && y.WithinIE(0, height); }
   }

   public class SquareCell
   {
      public readonly GridPosition position;

      public SquareCell(GridPosition position)
      {
         this.position = position;
      }
   }
}
