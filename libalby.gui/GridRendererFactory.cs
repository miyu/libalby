using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shade.Alby;

namespace Alby.Gui
{
   public class GridRendererFactory : IGridVisitor
   {
      private GridRenderer result;

      public GridRenderer CreateRenderer(Grid grid)
      {
         if (grid == null)
            result = new NullGridRenderer(grid);
         else 
            grid.Accept(this);
         return result;
      }

      public void Visit(SquareGrid grid) { this.result = new SquareGridRenderer(grid); }
   }

   public class NullGridRenderer : GridRenderer
   {
      public NullGridRenderer(Grid grid) : base(grid) {
      }

      public override Size GetRenderedSize(float zoom) { return new Size((int)Math.Ceiling(100.0f * zoom), (int)Math.Ceiling(100.0f * zoom)); }

      protected override void PaintGrid(float zoom)
      {
         RenderTarget.Graphics.Clear(Color.Black);
      }

      protected override void PaintDebugContext(float zoom)
      {
         
      }
   }
}
