using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using Shade.Alby;

namespace Alby.Gui
{
   public abstract class GridRenderer
   {
      private readonly Grid grid;
      private GridRenderTarget gridRenderTarget;
      private DebugGraphicsContext debugGraphicsContext;

      protected GridRenderer(Grid grid) { this.grid = grid; }

      public void Render(GridRenderTarget gridRenderTarget, float zoom, DebugGraphicsContext debugGraphicsContext)
      {
         this.gridRenderTarget = gridRenderTarget;
         this.debugGraphicsContext = debugGraphicsContext;

         gridRenderTarget.RequireSize(GetRenderedSize(zoom));
         gridRenderTarget.BeginPaint();
         PaintGrid(zoom);
         if (debugGraphicsContext != null)
            PaintDebugContext(zoom);
         gridRenderTarget.EndPaint();
      }

      protected GridRenderTarget RenderTarget { get { return gridRenderTarget; } }
      protected DebugGraphicsContext DebugGraphicsContext { get { return debugGraphicsContext; } }

      public abstract Size GetRenderedSize(float zoom);
      protected abstract void PaintGrid(float zoom);
      protected abstract void PaintDebugContext(float zoom);
   }
}