using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shade.Alby
{
   public class SquareGridManipulator
   {
      private SquareGrid grid;
      private DebugGraphicsContext debugGraphicsContext = null;
      public SquareGridManipulator(SquareGrid grid) { this.grid = grid; }
      public void SetDebugGraphicsContext(DebugGraphicsContext value) { this.debugGraphicsContext = value; }

      public void CutLine(float x1, float y1, float x2, float y2)
      {
         Console.WriteLine("!" + (debugGraphicsContext == null));
         Console.WriteLine("Cutline {0},{1} {2},{3}", x1, y1, x2, y2);
         if (debugGraphicsContext != null) {
            debugGraphicsContext.Line(new PointF(x1, y1), new PointF(x2, y2));
         }
         if (x1 > x2) {
            Console.WriteLine("RECURSIVE");
            CutLine(x2, y2, x1, y1);
         } else {
            // detect snapped endpoints
            CutSnappedPoint(x1, y1);
            CutSnappedPoint(x2, y2);

            // note: this assumes down-right-ward
            var dx = x2 - x1;
            var dy = y2 - y1;

            bool upward = dy > 0;

            int cx = (int)Math.Floor(x1), cy = upward ? (int)Math.Floor(y1) : (int)Math.Ceiling(y1);
            float x = x1, y = y1;
            var e = upward ? y - cy : cy - y;
            var dedx = Math.Abs(dy / dx);
            var dxde = 1.0f / dedx;
            var yStep = upward ? 1 : -1;

            if (Math.Abs(dx) < 0.001) {
               CutVerticalLine(x1, y1, x2, y2);
               return;
            }

            while (x < x2 || (y < y2 == upward)) {
               int nextX = cx + 1;
               var deToDx = dedx * (nextX - x);
               Console.WriteLine("AT " + cx + " " + cy + " with " + x + " " + y);

               // Hit horizontal connector
               if (e + deToDx >= 1.0f) {
                  var dxToNextE = (1.0f - e) * dxde;
                  var nextY = y + (dy / dx) * dxToNextE;
                  
                  if ((nextY > y2) == upward) {
                     // we're done
                     break;
                  }

                  x += dxToNextE;
                  y = nextY;
                  e = 0;

                  if (debugGraphicsContext != null)
                     debugGraphicsContext.PlotPoint(new PointF(x, y));
                  
                  cy += yStep;
                  grid.GetCell(cx, cy).EastConnector.Break();

                  Console.WriteLine(" => " + cx + " " + cy + " with " + x + " " + y);
               } else {
                  if (nextX > x2) {
                     // we're done
                     break;
                  }

                  // hit vertical connector
                  var dxToNextX = nextX - x;
                  x = nextX;
                  y += dxToNextX * dy / dx;
                  e += dedx * dxToNextX;

                  if (debugGraphicsContext != null)
                     debugGraphicsContext.PlotPoint(new PointF(x, y));

                  cx++;
                  if (upward) {
                     grid.GetCell(cx, cy).SouthConnector.Break();
                  } else {
                     grid.GetCell(cx, cy).NorthConnector.Break();
                  }

                  Console.WriteLine(" => " + cx + " " + cy + " with " + x + " " + y);
               }
            }
         }
      }

      private void CutSnappedPoint(float x1, float y1)
      {
         int snapX = (int)Math.Round(x1);
         int snapY = (int)Math.Round(y1);
         if (Math.Abs(x1 - snapX) < 0.001) {
            var y = (int)Math.Floor(y1);
            grid.GetCell(snapX, y).SouthConnector.Break();
         }
         if (Math.Abs(y1 - snapY) < 0.001) {
            var x = (int)Math.Floor(x1);
            grid.GetCell(x, snapY).EastConnector.Break();
         }
      }

      private void CutVerticalLine(float x1, float y1, float x2, float y2)
      {
         if (y1 > y2) {
            CutVerticalLine(x2, y2, x1, y1);
         } else {
            var x = (int)Math.Floor(x1);
            var startY = (int)Math.Ceiling(y1);
            var endY = (int)Math.Floor(y2);

            for (var y = startY; y <= endY; y++) {
               grid.GetCell(x, y).EastConnector.Break();
            }
         }
      }
   }
}
