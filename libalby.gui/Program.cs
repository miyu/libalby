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
         this.manipulator.CutParametric(new SpiralParametricFunction(0.75f, 10.5f, 3.0f, 25, 15));
         this.manipulator.CutLine(25 + 3 * 3.5f, 15, 25 + 3 * 2.5f, 15);
         this.manipulator.FillRegion(25, 15);
      }
   }

   public class SimpleRendererModel
   {
      private SquareGrid grid;
   }
}
