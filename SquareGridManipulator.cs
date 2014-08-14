using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
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

      public void CutLine(PointF p1, PointF p2) { CutLine(p1.X, p1.Y, p2.X, p2.Y); }

      public void CutLine(float x1, float y1, float x2, float y2)
      {
         if (debugGraphicsContext != null) {
            debugGraphicsContext.Line(new PointF(x1, y1), new PointF(x2, y2));
         }
         if (x1 > x2) {
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

                  var cell = grid.GetCellOrNull(cx, cy);
                  if (cell != null) {
                     if (cell.EastConnector != null) {
                        cell.EastConnector.Break();
                     }
                  }
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
                  var cell = grid.GetCellOrNull(cx, cy);
                  if (cell != null) {
                     if (upward) {
                        if (cell.SouthConnector != null) {
                           cell.SouthConnector.Break();
                        }
                     } else {
                        if (cell.NorthConnector != null) {
                           cell.NorthConnector.Break();
                        }
                     }
                  }
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
            var cell = grid.GetCellOrNull(snapX, y);
            if (cell != null && cell.SouthConnector != null) {
               cell.SouthConnector.Break();
            }
         }
         if (Math.Abs(y1 - snapY) < 0.001) {
            var x = (int)Math.Floor(x1);
            var cell = grid.GetCellOrNull(x, snapY);
            if (cell != null && cell.EastConnector != null) {
               cell.EastConnector.Break();
            }
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

      public void CutParametric(ParametricFunction parametricFunction)
      {
         PointF? lastPoint = null;
         for (float t = parametricFunction.TInitial; t <= parametricFunction.TFinal; t = parametricFunction.NextT(t)) {
            var point = parametricFunction.PointAt(t);
            if (lastPoint != null) {
               CutLine(lastPoint.Value, point);
            }
            lastPoint = point;
         }
      }
   }
}
