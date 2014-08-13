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
         //this.controller.DrawTrapezoid();
         this.controller.DrawSpiral();
         //this.controller.DrawHorizontalLine();
         //this.controller.DrawVerticalLine();
         //this.controller.DrawHorizontalLineSnapped();
         //this.controller.DrawVerticalLineSnapped();
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
         var drdt = 0.7f;
         var d0dt = 2 * pi / 6.0f;
         var xc = 25;
         var yc = 15;
         Func<float, PointF> f = (t) => {
            var r = drdt * t;
            var theta = d0dt * t;
            return new PointF(xc + r * (float)Math.Sin(theta), yc + r * (float)Math.Cos(theta));
         };
         PointF? last = null;
         for(var t = 1f; t < 6f; t += 0.05f) {
            var point = f(t);

            if (last != null) {
               var x1 = point.X;
               var y1 = point.Y;
               var x2 = last.Value.X;
               var y2 = last.Value.Y;
               this.manipulator.CutLine(x1, y1, x2, y2);
            }
            last = point;
         }
      }
   }

   public class SimpleRendererModel
   {
      private SquareGrid grid;
   }
}
