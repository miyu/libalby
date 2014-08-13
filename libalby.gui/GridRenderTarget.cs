using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alby.Gui
{
   public class GridRenderTarget
   {
      private Bitmap bitmap;
      private Size size;
      private Graphics graphics;

      public GridRenderTarget()
      {
      }

      public void RequireSize(Size size)
      {
         if (this.bitmap == null || size != this.size) {
            this.size = size;
            this.bitmap = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppRgb);
         }
      }

      public void BeginPaint() { this.graphics = Graphics.FromImage(this.bitmap); }
      public void EndPaint() { this.graphics.Dispose(); this.graphics = null; }

      public Graphics Graphics { get { return this.graphics; } }
      public Image Bitmap { get { return this.bitmap; } }
   }
}
