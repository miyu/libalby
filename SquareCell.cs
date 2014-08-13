
namespace Shade.Alby
{
   public class SquareCell : Cell
   {
      private readonly SquareGrid grid;

      private CellConnector northConnector;
      private CellConnector southConnector;
      private CellConnector eastConnector;
      private CellConnector westConnector;

      public SquareCell(SquareGrid grid, GridPosition position)
         : base(position) { this.grid = grid; }
      
      public SquareCell NorthNeighbor { get { return grid.GetCellOrNull(Position.X, Position.Y - 1); }}
      public SquareCell SouthNeighbor { get { return grid.GetCellOrNull(Position.X, Position.Y + 1); } }
      public SquareCell WestNeighbor { get { return grid.GetCellOrNull(Position.X - 1, Position.Y); } }
      public SquareCell EastNeighbor { get { return grid.GetCellOrNull(Position.X + 1, Position.Y); }}
      
      public CellConnector NorthConnector { get { return northConnector; }}
      public CellConnector SouthConnector { get { return southConnector; }}
      public CellConnector EastConnector { get { return eastConnector; }}
      public CellConnector WestConnector { get { return westConnector; }}

      public void SetNorthConnector(CellConnector connector) { northConnector = connector; }
      public void SetSouthConnector(CellConnector connector) { southConnector = connector; }
      public void SetEastConnector(CellConnector connector) { eastConnector = connector; }
      public void SetWestConnector(CellConnector connector) { westConnector = connector; }
   }
}