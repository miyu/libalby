using System.Collections.Generic;

namespace Shade.Alby
{
   public abstract class Cell
   {
      private readonly GridPosition position;
      private SquareCell[] neighbors;

      protected Cell(GridPosition position)
      {
         this.position = position;
      }

      public GridPosition Position { get { return position; } }
      public SquareCell[] Neighbors { get { return neighbors; } }
      public abstract IReadOnlyCollection<CellConnector> Connectors { get; }

      public void SetNeighbors(SquareCell[] neighbors) { this.neighbors = neighbors; }
   }
}
