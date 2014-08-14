using System;
using System.Drawing;
using System.Windows.Forms;
using Shade.Alby;

namespace Alby.Gui
{
   public class Program
   {
      private static void Main(string[] args)
      {
         Application.EnableVisualStyles();

         new SimpleRendererGame().Run();
      }
   }

   public class SimpleRendererGame
   {
      private readonly SimpleRendererController controller;
      private readonly MainForm form;

      public SimpleRendererGame()
      {
         this.form = new MainForm();
         this.controller = new SimpleRendererController(form);
         this.controller.GenerateGrid();
         this.controller.DrawSpiral();
      }

      public void Run()
      {
         Application.Run(this.form);
      }
   }

   public class SimpleRendererController
   {
      private MainForm form;
      private SquareGridManipulator manipulator;
      private DebugGraphicsContext context = new DebugGraphicsContext();

      public SimpleRendererController(MainForm form) { this.form = form; }

      public void GenerateGrid(int width = 50, int height = 30)
      {
         var grid = new SquareGrid(width, height);
         this.manipulator = new SquareGridManipulator(grid);
         this.manipulator.SetDebugGraphicsContext(context);
         this.form.SetDebugGraphicsContext(context);
         this.form.SetGrid(grid);
      }

      public void DrawTrapezoid()
      {
         this.manipulator.CutLine(1.3f, 1.5f, 1.9f, 5.6f);
         this.manipulator.CutLine(1.3f, 1.5f, 4.9f, 3.6f);
         this.manipulator.CutLine(1.9f, 5.6f, 4.6f, 4.6f);
         this.manipulator.CutLine(4.6f, 4.6f, 4.9f, 3.6f);
      }

      public void DrawHorizontalLine()
      {
         this.manipulator.CutLine(0.5f, 6.5f, 3.5f, 6.5f);
      }
      public void DrawHorizontalLineSnapped()
      {
         this.manipulator.CutLine(0.5f, 9f, 3.5f, 9f);
      }
      public void DrawVerticalLine()
      {
         this.manipulator.CutLine(6.5f, 0.5f, 6.5f, 3.5f);
      }
      public void DrawVerticalLineSnapped()
      {
         this.manipulator.CutLine(9f, 0.5f, 9f, 3.5f);
      }

      public void DrawSpiral()
      {
         var pi = (float)Math.PI;
         var drdt = 1.3f;
         var d0dt = 2 * pi / 6.0f;
         var xc = 25;
         var yc = 15;
         var rInitial = 1.5f;
         Func<float, PointF> f = (t) =>
         {
            var r = rInitial + drdt * t;
            var theta = d0dt * t;
            return new PointF(xc + r * (float)Math.Sin(theta), yc + r * (float)Math.Cos(theta));
         };
         Func<float, PointF> f2 = (t) => {
            var dtPerRevolution = 2 * pi / d0dt;
            var drPerRevolution = drdt * dtPerRevolution;
            var r = rInitial + drdt * t;
            var theta = d0dt * t + Math.PI;
            return new PointF(xc + r * (float)Math.Sin(theta), yc + r * (float)Math.Cos(theta));
         };
         PointF? lastF = null;
         PointF? lastF2 = null;
         for (var t = 0f; t < 30f; t += 0.05f) {
            var point = f(t);
            if (lastF != null) {
               this.manipulator.CutLine(point.X, point.Y, lastF.Value.X, lastF.Value.Y);
            }
            lastF = point;

            var point2 = f2(t);
            if (lastF2 != null) {
               this.manipulator.CutLine(point2.X, point2.Y, lastF2.Value.X, lastF2.Value.Y);
            }

            lastF2 = point2;
         }
      }
   }

   public class SimpleRendererModel
   {
      private SquareGrid grid;
   }
}
