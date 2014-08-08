namespace Shade.Alby
{
   public class GridPosition
   {
      public int X;
      public int Y;

      public GridPosition()
      {
         X = 0;
         Y = 0;
      }

      public GridPosition(int x, int y)
      {
         X = x;
         Y = y;
      }

      public override int GetHashCode()
      {
         var hash = 13;
         hash = hash * 31 + X * 17;
         hash = hash * 31 + Y * 17;
         return hash;
      }

      public override bool Equals(object obj)
      {
         var gridPosition = obj as GridPosition;
         if (gridPosition == null) {
            return false;
         } else {
            return X == gridPosition.X && Y == gridPosition.Y;
         }
      }

      public override string ToString()
      {
         return "[GridPosition{X:" + X + ",Y:" + Y + "}]";
      }
   }
}
