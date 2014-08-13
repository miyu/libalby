using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using ItzWarty;

namespace Shade.Alby
{
   public abstract class Grid
   {
      protected int width;
      protected int height;
      protected IReadOnlyList<SquareCell> cells = new List<SquareCell>();

      public int Width { get { return width; } }
      public int Height { get { return height; } }
      public IReadOnlyList<SquareCell> Cells { get { return cells; } }

      public abstract SquareCell GetCell(int x, int y);
      public SquareCell GetCell(GridPosition position) { return GetCell(position.X, position.Y); }

      public abstract SquareCell GetCellOrNull(int x, int y);
      public SquareCell GetCellOrNull(GridPosition position) { return GetCell(position.X, position.Y); }

      public abstract bool IsValidCell(int x, int y);
      public bool IsValidCell(GridPosition position) { return IsValidCell(position.X, position.Y); }

      public abstract IEnumerable<GridPosition> GetNeighboringCellPositions(int x, int y);
      public IEnumerable<GridPosition> GetNeighboringCellPositions(GridPosition position) { return GetNeighboringCellPositions(position.X, position.Y); }
      public IEnumerable<SquareCell> GetNeighboringCells(GridPosition position) { return GetNeighboringCellPositions(position).Select(GetCell); }

      public abstract void Accept(IGridVisitor visitor);
   }

   public class SquareGrid : Grid
   {
      public SquareGrid(int width, int height)
      {
         this.width = width;
         this.height = height;

         cells = Util.Generate(height, width, (y, x) => new SquareCell(this, new GridPosition(x, y)));
         foreach (var cell in cells) {
            cell.SetNeighbors(GetNeighboringCells(cell.Position).ToArray());
         }
         for (var y = 0; y < height; y++) {
            for (var x = 0; x < width; x++) {
               var cell = GetCell(x, y);
               if (x != 0) {
                  var other = cell.WestNeighbor;
                  var connector = new CellConnector(cell, other);
                  cell.SetWestConnector(connector);
                  other.SetEastConnector(connector);
               }
               if (y != 0) {
                  var other = cell.NorthNeighbor;
                  var connector = new CellConnector(cell, other);
                  cell.SetNorthConnector(connector);
                  other.SetSouthConnector(connector);
               }
            }
         }
      }

      public override SquareCell GetCell(int x, int y) { return this.cells[y * width + x]; }

      public override SquareCell GetCellOrNull(int x, int y)
      {
         if (!x.WithinIE(0, width) || !y.WithinIE(0, height))
            return null;
         else
            return this.cells[y * width + x];
      }

      public override bool IsValidCell(int x, int y) { return x.WithinIE(0, width) && y.WithinIE(0, height); }
      
      public override IEnumerable<GridPosition> GetNeighboringCellPositions(int x, int y)
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

      public override void Accept(IGridVisitor visitor)
      {
         visitor.Visit(this);
      }
   }
}
