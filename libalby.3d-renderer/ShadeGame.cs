using Shade.Alby;
using SharpDX;
using SharpDX.Toolkit;

namespace Alby.Gui
{
   public class ShadeGame : Game
   {
      private GraphicsDeviceManager m_graphicsDeviceManager;
      private CameraProvider m_cameraProvider;
      private SceneRenderer m_sceneRenderer;

      public ShadeGame()
      {
         m_graphicsDeviceManager = new GraphicsDeviceManager(this);
         m_sceneRenderer = new SceneRenderer(this);
         m_cameraProvider = new CameraProvider(this);

         Content.RootDirectory = "Content";
      }

      protected override void Initialize()
      {
         base.Initialize();

         var grid = new SquareGrid(50, 30);
         var manipulator = new SquareGridManipulator(grid);
         manipulator.CutParametric(new SpiralParametricFunction(0.75f, 10.5f, 3.0f, 25, 15));
         manipulator.CutLine(25 + 3 * 3.5f, 15, 25 + 3 * 2.5f, 15);
         manipulator.FillRegion(25, 15);

         m_sceneRenderer.ImportGrid(grid, 25, 15);
      }

      /// <summary>
      /// Draws game content.
      /// </summary>
      /// <param name="gameTime">Structure containing information about elapsed game time.</param>
      protected override void Draw(GameTime gameTime)
      {
         // clear the scene to a CornflowerBlue color
         GraphicsDevice.Clear(Color.CornflowerBlue);

         base.Draw(gameTime);
      }
   }
}