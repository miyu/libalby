using System;
using System.Collections.Generic;
using ItzWarty.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Shade.Alby
{
   [TestClass]
   public class SquareGridTest : MockitoLike
   {
      private SquareGrid testObj;

      private const int GRID_WIDTH = 10;
      private const int GRID_HEIGHT = 10;

      [TestInitialize]
      public void Setup()
      {
         InitializeMocks();

         testObj = new SquareGrid(GRID_WIDTH, GRID_HEIGHT);
      }

      [TestMethod]
      public void GetCellGetsCorrectCell()
      {
         for (var y = 0; y < GRID_HEIGHT; y++)
         {
            for (var x = 0; x < GRID_WIDTH; x++)
            {
               var cell = testObj.GetCell(x, y);
               Console.WriteLine("Cell {0} {1} has {2} {3}", x, y, cell.Position.X, cell.Position.Y);
               assertEquals(x, cell.Position.X);
               assertEquals(y, cell.Position.Y);
            }
         }
      }

      [TestMethod]
      public void TestGetTopLeftNeighborPositions()
      {
         var neighbors = testObj.GetNeighboringCellPositions(new GridPosition(0, 0));
         var expected = new HashSet<GridPosition> {
            new GridPosition(1, 0),
            new GridPosition(0, 1)
         };
         assertTrue(expected.SetEquals(neighbors));
      }

      [TestMethod]
      public void TestGetMiddleLeftNeighborPositions()
      {
         var neighbors = testObj.GetNeighboringCellPositions(new GridPosition(0, GRID_HEIGHT / 2));
         var expected = new HashSet<GridPosition> {
            new GridPosition(0, GRID_HEIGHT / 2 - 1),
            new GridPosition(1, GRID_HEIGHT / 2),
            new GridPosition(0, GRID_HEIGHT / 2 + 1),
         };
         assertTrue(expected.SetEquals(neighbors));
      }

      [TestMethod]
      public void TestGetBottomLeftNeighborPositions()
      {
         var neighbors = testObj.GetNeighboringCellPositions(new GridPosition(0, GRID_HEIGHT - 1));
         var expected = new HashSet<GridPosition> {
            new GridPosition(0, GRID_HEIGHT - 2),
            new GridPosition(1, GRID_HEIGHT - 1)
         };
         assertTrue(expected.SetEquals(neighbors));
      }

      [TestMethod]
      public void TestGetTopMiddleNeighborPositions()
      {
         var neighbors = testObj.GetNeighboringCellPositions(new GridPosition(GRID_WIDTH / 2, 0));
         var expected = new HashSet<GridPosition> {
            new GridPosition(GRID_WIDTH / 2 - 1, 0),
            new GridPosition(GRID_WIDTH / 2, 1),
            new GridPosition(GRID_WIDTH / 2 + 1, 0)
         };
         assertTrue(expected.SetEquals(neighbors));
      }

      [TestMethod]
      public void TestGetTopRightNeighborPositions()
      {
         var neighbors = testObj.GetNeighboringCellPositions(new GridPosition(GRID_WIDTH - 1, 0));
         var expected = new HashSet<GridPosition> {
            new GridPosition(GRID_WIDTH - 2, 0),
            new GridPosition(GRID_WIDTH - 1, 1)
         };
         assertTrue(expected.SetEquals(neighbors));
      }

      [TestMethod]
      public void TestGetMiddleRightNeighborPositions()
      {
         var neighbors = testObj.GetNeighboringCellPositions(new GridPosition(GRID_WIDTH - 1, GRID_HEIGHT / 2));
         var expected = new HashSet<GridPosition> {
            new GridPosition(GRID_WIDTH - 1, GRID_HEIGHT / 2 - 1),
            new GridPosition(GRID_WIDTH - 2, GRID_HEIGHT / 2),
            new GridPosition(GRID_WIDTH - 1, GRID_HEIGHT / 2 + 1),
         };
         assertTrue(expected.SetEquals(neighbors));
      }

      [TestMethod]
      public void TestGetBottomRightNeighborPositions()
      {
         var neighbors = testObj.GetNeighboringCellPositions(new GridPosition(GRID_WIDTH - 1, GRID_HEIGHT - 1));
         var expected = new HashSet<GridPosition> {
            new GridPosition(GRID_WIDTH - 1, GRID_HEIGHT - 2),
            new GridPosition(GRID_WIDTH - 2, GRID_HEIGHT - 1)
         };
         assertTrue(expected.SetEquals(neighbors));
      }

      [TestMethod]
      public void TestGetBottomMiddleNeighborPositions()
      {
         var neighbors = testObj.GetNeighboringCellPositions(new GridPosition(GRID_WIDTH / 2, GRID_HEIGHT - 1));
         var expected = new HashSet<GridPosition> {
            new GridPosition(GRID_WIDTH / 2 - 1, GRID_HEIGHT - 1),
            new GridPosition(GRID_WIDTH / 2, GRID_HEIGHT - 2),
            new GridPosition(GRID_WIDTH / 2 + 1, GRID_HEIGHT - 1)
         };
         assertTrue(expected.SetEquals(neighbors));
      }

      [TestMethod]
      public void TestGetMiddleMiddleNeighborPositions()
      {
         var neighbors = testObj.GetNeighboringCellPositions(new GridPosition(GRID_WIDTH / 2, GRID_HEIGHT / 2));
         var expected = new HashSet<GridPosition> {
            new GridPosition(GRID_WIDTH / 2, GRID_HEIGHT / 2 - 1),
            new GridPosition(GRID_WIDTH / 2 - 1, GRID_HEIGHT / 2),
            new GridPosition(GRID_WIDTH / 2 + 1, GRID_HEIGHT / 2),
            new GridPosition(GRID_WIDTH / 2, GRID_HEIGHT / 2 + 1)
         };
         assertTrue(expected.SetEquals(neighbors));
         
      }
   }
}
