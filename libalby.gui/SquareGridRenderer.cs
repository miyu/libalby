using System;
using System.Drawing;
using Shade.Alby;

namespace Alby.Gui
{
   public class SquareGridRenderer : GridRenderer
   {
      private const float CELL_SIZE = 1.0f;
      private const float CONNECTOR_LENGTH = 15.0f;
      private const float CONNECTOR_WIDTH = 2.50f;

      private const float DEBUG_POINT_SIZE = 3f;
      private const float DEBUG_LINE_SIZE = 2f;

      private readonly SquareGrid squareGrid;

      public SquareGridRenderer(SquareGrid grid)
         : base(grid) { squareGrid = grid; }

      public override Size GetRenderedSize(float zoom)
      {
         int width = (int)Math.Ceiling((squareGrid.Width * CELL_SIZE + (squareGrid.Width - 1) * CONNECTOR_LENGTH) * zoom) + 1;
         int height = (int)Math.Ceiling((squareGrid.Height * CELL_SIZE + (squareGrid.Height - 1) * CONNECTOR_LENGTH) * zoom) + 1;
         return new Size(width, height);
      }

      protected override void PaintGrid(float zoom)
      {
         var g = RenderTarget.Graphics;
         //g.Clear(Color.Black);

         foreach (var cell in squareGrid.Cells) {
            var left = (cell.Position.X * (CELL_SIZE + CONNECTOR_LENGTH)) * zoom;
            var top = (cell.Position.Y * (CELL_SIZE + CONNECTOR_LENGTH)) * zoom;
            var width = CELL_SIZE * zoom;
            var height = CELL_SIZE * zoom;
            float right = left + width, bottom = top + height;
            g.DrawRectangle(Pens.White, left, top, width, height);

            if (cell.EastConnector != null) {
               var hcWidth = CONNECTOR_LENGTH * zoom;
               var hcHeight = CONNECTOR_WIDTH * zoom;
               var hcLeft = right;
               var hcTop = top + (height - hcHeight) / 2.0f;
               g.DrawRectangle(GetConnectorStatePen(cell.EastConnector.State), hcLeft, hcTop, hcWidth, hcHeight);
            }

            if (cell.SouthConnector != null) {
               var vcWidth = CONNECTOR_WIDTH * zoom;
               var vcHeight = CONNECTOR_LENGTH * zoom;
               var vcLeft = left + (width - vcWidth) / 2.0f;
               var vcTop = bottom;
               g.DrawRectangle(GetConnectorStatePen(cell.SouthConnector.State), vcLeft, vcTop, vcWidth, vcHeight);
            }
         }
      }

      protected override void PaintDebugContext(float zoom)
      {
         using (var pen = new Pen(Brushes.Red, DEBUG_LINE_SIZE * zoom)) {
            var pointSize = DEBUG_POINT_SIZE * zoom;
            var lineSize = DEBUG_LINE_SIZE * zoom;
            var tileCenterSpacing = (CELL_SIZE + CONNECTOR_LENGTH) * zoom;
            var tileCenterOffset = ((CELL_SIZE) / 2.0f) * zoom;

            foreach (var line in DebugGraphicsContext.Lines) {
               var start = line[0];
               var end = line[1];
               var x1 = tileCenterOffset + tileCenterSpacing * start.X;
               var y1 = tileCenterOffset + tileCenterSpacing * start.Y;
               var x2 = tileCenterOffset + tileCenterSpacing * end.X;
               var y2 = tileCenterOffset + tileCenterSpacing * end.Y;
               RenderTarget.Graphics.DrawLine(pen, x1, y1, x2, y2);
            }

            foreach (var point in DebugGraphicsContext.Points) {
               var x = tileCenterOffset + tileCenterSpacing * point.X - pointSize / 2.0f;
               var y = tileCenterOffset + tileCenterSpacing * point.Y - pointSize / 2.0f;
               RenderTarget.Graphics.FillRectangle(Brushes.Lime, x, y, pointSize + 1, pointSize + 1);
            }
         }
      }

      private Pen GetConnectorStatePen(ConnectorState connectorState)
      {
         if (connectorState == ConnectorState.Connected)
            return Pens.Lime;
         else if (connectorState == ConnectorState.Disconnected)
            return Pens.Gray;
         else if (connectorState == ConnectorState.Broken)
            return Pens.Red;
         else
            return Pens.DarkGray;
      }
   }
}