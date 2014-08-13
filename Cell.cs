using System.Collections.Generic;

namespace Shade.Alby
{
   public class Cell
   {
      private readonly GridPosition position;
      private SquareCell[] neighbors;
      private List<CellConnector> connections;

      public Cell(GridPosition position)
      {
         this.position = position;
      }

      public GridPosition Position { get { return position; } }
      public SquareCell[] Neighbors { get { return neighbors; } }
      public IReadOnlyCollection<CellConnector> Connections { get { return connections; } }

      public void SetNeighbors(SquareCell[] neighbors) { this.neighbors = neighbors; }
   }
}
