using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Shade.Alby;

namespace Alby.Gui
{
   public sealed class GridDisplay : Control
   {
      private readonly GridRendererFactory gridRendererFactory = new GridRendererFactory();
      private readonly GridRenderTarget renderTarget = new GridRenderTarget();

      private Grid grid;
      private float zoom = 1.0f;
      private DebugGraphicsContext debugGraphicsContext;

      public GridDisplay()
      {
         this.MinimumSize = new Size(100, 100);
         this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
         this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
         this.SetStyle(ControlStyles.DoubleBuffer, true);
         this.SetStyle(ControlStyles.Opaque, true);
         this.SetStyle(ControlStyles.UserPaint, true);
      }

      protected override void OnPaint(PaintEventArgs e)
      {
         base.OnPaint(e);

         Render();
         e.Graphics.DrawImage(renderTarget.Bitmap, e.ClipRectangle, e.ClipRectangle, GraphicsUnit.Pixel);
      }

      public float Zoom { get { return zoom; } set { zoom = value; } }

      public void SetGrid(Grid grid)
      {
         this.grid = grid;
         Invalidate();
      }

      private void Render()
      {
         var renderer = new GridRendererFactory().CreateRenderer(this.grid);
         renderer.Render(renderTarget, zoom, debugGraphicsContext);
         this.Size = renderer.GetRenderedSize(zoom);
      }

      public void SetDebugGraphicsContext(DebugGraphicsContext context) { this.debugGraphicsContext = context; }
   }
}
