using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shade.Alby
{
   public abstract class ParametricFunction
   {
      protected readonly float tInitial;
      protected readonly float tFinal;

      protected ParametricFunction(float tInitial, float tFinal)
      {
         this.tInitial = tInitial;
         this.tFinal = tFinal;
      }

      public float TInitial { get { return tInitial; } }
      public float TFinal { get { return tFinal; } }
      public virtual float NextT(float t) { return t + 0.3f; }
      public abstract PointF PointAt(float t);
   }
}
