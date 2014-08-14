using System;
using System.Drawing;

namespace Shade.Alby
{
   public class SpiralParametricFunction : ParametricFunction
   {
      private const float pi = (float)Math.PI;
      private const float kStepsPerRevolutionPerUnitRadius = 100f;

      private readonly float rInitial, drdt;
      private readonly float thetaInitial, d0dt;
      private readonly float xCenter, yCenter;

      public SpiralParametricFunction(float rInner, float rOuter, float drPerRevolution, float xCenter, float yCenter, float thetaInitial = 0.0f)
         : base(0.0f, ComputeTFinal(rInner, rOuter, drPerRevolution))
      {
         float revolutions = (rOuter - rInner) / drPerRevolution;
         float thetaFinal = thetaInitial + revolutions * 2 * pi;
         this.rInitial = rInner;
         this.drdt = (rOuter - rInner) / (tFinal - tInitial);
         this.thetaInitial = thetaInitial;
         this.d0dt = (thetaFinal - thetaInitial) / (tFinal - tInitial);
         this.xCenter = xCenter;
         this.yCenter = yCenter;
      }

      private static float ComputeTFinal(float rInner, float rOuter, float drPerRevolution)
      {
         float kStepsPerRevolution = rOuter * kStepsPerRevolutionPerUnitRadius;
         float revolutions = (rOuter - rInner) / drPerRevolution;
         return revolutions * kStepsPerRevolution;
      }

      public override PointF PointAt(float t)
      {
         var r = rInitial + drdt * t;
         var theta = thetaInitial + d0dt * t;
         return new PointF(xCenter + r * (float)Math.Sin(theta), yCenter + r * (float)Math.Cos(theta));
      }
   }
}
