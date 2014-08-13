using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shade.Alby
{
   public class DebugGraphicsContext
   {
      private List<PointF> points = new List<PointF>();
      private List<PointF[]> lines = new List<PointF[]>();
      public void Clear() { this.points.Clear(); this.lines.Clear(); }
      public void PlotPoint(PointF point) { this.points.Add(point); }
      public void Line(PointF start, PointF end) { this.lines.Add(new PointF[] { start, end }); }

      public IReadOnlyList<PointF> Points { get { return points; } }
      public IReadOnlyList<PointF[]> Lines { get { return lines; } }
   }
}
