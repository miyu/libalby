using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shade.Alby;
using SharpDX.Toolkit.Graphics;

namespace Alby.Gui
{
   public class SquareCellRenderable : IRenderable
   {
      private SquareCell cell;
      private SceneRenderer renderer;

      public SquareCellRenderable(SquareCell cell, SceneRenderer renderer)
      {
         this.cell = cell;
         this.renderer = renderer;
      }

      public void Render()
      {
         var x = this.cell.Position.X;
         var y = this.cell.Position.Y;
         if (cell.Connectors.Any((c) => c.State == ConnectorState.Connected))
         {
            renderer.RenderPlane(this.cell.Position.X, this.cell.Position.Y);
            if (cell.EastConnector != null && cell.EastConnector.State == ConnectorState.Connected)
               renderer.RenderEastConnector(x, y);
            if (cell.SouthConnector != null && cell.SouthConnector.State == ConnectorState.Connected)
               renderer.RenderSouthConnector(x, y);
         }
      }
   }

   public interface IRenderable
   {
      void Render();
   }
}
