using System;
using System.Collections.Generic;
using Shade.Alby;
using SharpDX;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;

namespace Shade.Helios
{
   /// <summary>
   /// Component responsible for the scene rendering.
   /// </summary>
   public sealed class SceneRenderer : GameSystem
   {
      private ICameraService _cameraService;

      private GeometricPrimitive _cube;
      private Texture2D _cubeTexture;
      private Matrix _cubeTransform;

      private GeometricPrimitive _plane;
      private Texture2D _planeTexture;
      private Matrix _planeTransform;

      private BasicEffect _basicEffect;
      //private List<IRenderable> renderables = new List<IRenderable>();

      /// <summary>
      /// Initialize in constructor anything that doesn't depend on other services.
      /// </summary>
      /// <param name="game">The game where this system will be attached to.</param>
      public SceneRenderer(Game game)
         : base(game)
      {
         // this game system has something to draw - enable drawing by default
         // this can be disabled to make objects drawn by this system disappear
         Visible = true;

         // this game system has logic that needs to be updated - enable update by default
         // this can be disabled to simulate a "pause" in logic update
         Enabled = true;

         // add the system itself to the systems list, so that it will get initialized and processed properly
         // this can be done after game initialization - the Game class supports adding and removing of game systems dynamically
         game.GameSystems.Add(this);
      }

      /// <summary>
      /// Initialize here anything that depends on other services
      /// </summary>
      public override void Initialize()
      {
         base.Initialize();

         // get the camera service from service registry
         _cameraService = Services.GetService<ICameraService>();
      }

      /// <summary>
      /// Load all graphics content here.
      /// </summary>
      protected override void LoadContent()
      {
         base.LoadContent();

         // initialize the basic effect (shader) to draw the geometry, the BasicEffect class is similar to one from XNA
         _basicEffect = ToDisposeContent(new BasicEffect(GraphicsDevice));

         _basicEffect.EnableDefaultLighting(); // enable default lightning, useful for quick prototyping
         _basicEffect.TextureEnabled = true;   // enable texture drawing

         LoadCube();
         LoadPlane();
         LoadConnector();
      }

      /// <summary>
      /// Draw the scene content.
      /// </summary>
      /// <param name="gameTime">Structure containing information about elapsed game time.</param>
      public override void Draw(GameTime gameTime)
      {
         base.Draw(gameTime);

         // set the parameters for cube drawing and draw it using the basic effect
         _basicEffect.Texture = _cubeTexture;
         _basicEffect.World = _cubeTransform;
         _cube.Draw(_basicEffect);

         _basicEffect.Texture = _planeTexture;
         _basicEffect.World = Matrix.RotationX(-MathUtil.PiOverTwo) * Matrix.Translation(2, 0, 2f);
         _plane.Draw(_basicEffect);  
      }

      /// <summary>
      /// Update the scene logic.
      /// </summary>
      /// <param name="gameTime">Structure containing information about elapsed game time.</param>
      public override void Update(GameTime gameTime)
      {
         base.Update(gameTime);

         // get the total elapsed seconds since the start of the game
         var time = (float)gameTime.TotalGameTime.TotalSeconds;

         // update the cube position to add some movement
         _cubeTransform = Matrix.RotationX(time) * Matrix.RotationY(time * 2f) * Matrix.RotationZ(time * .7f);

         // update view and projection matrices from the camera service
         _basicEffect.View = _cameraService.View;
         _basicEffect.Projection = _cameraService.Projection;
      }

      /// <summary>
      /// Loads cube geometry and anything related.
      /// </summary>
      private void LoadCube()
      {
         // build the cube geometry and mark it disposable when content will be uploaded
         _cube = ToDisposeContent(GeometricPrimitive.Cube.New(GraphicsDevice));

         // load the texture using game's content manager
         _cubeTexture = Content.Load<Texture2D>("logo_large.png");

         // the cube's transform will be updated in runtime
         _cubeTransform = Matrix.Identity;
      }

      /// <summary>
      /// Loads plane geometry and anything related.
      /// </summary>
      private void LoadPlane()
      {
         // build the plane geometry of the specified size and subdivision segments
         _plane = ToDisposeContent(GeometricPrimitive.Plane.New(GraphicsDevice, 1f, 1f));

         // load the texture using game's content manager
         _planeTexture = Content.Load<Texture2D>("GeneticaMortarlessBlocks.jpg");

         // rotate the plane horizontally and move it down a bit
         _planeTransform = Matrix.RotationX(-MathUtil.PiOverTwo) * Matrix.Translation(0f, 0f, 0f);
      }

      /// <summary>
      /// Loads plane geometry and anything related.
      /// </summary>
      private void LoadConnector()
      {
         // build the plane geometry of the specified size and subdivision segments
         //_hConnector = ToDisposeContent(GeometricPrimitive.Plane.New(GraphicsDevice, 0.2f, 0.15f));
         //_vConnector = ToDisposeContent(GeometricPrimitive.Plane.New(GraphicsDevice, 0.15f, 0.2f));

         //_connectedTexture = Content.Load<Texture2D>("lime.jpg");

         // load the texture using game's content manager
         //_planeTexture = Content.Load<Texture2D>("GeneticaMortarlessBlocks.jpg");

         // rotate the plane horizontally and move it down a bit
         _planeTransform = Matrix.RotationX(-MathUtil.PiOverTwo) * Matrix.Translation(0f, 0f, 0f);
      }

//      private float planeOffset = 0;
//      private float planeOffsty = 0;
//      private float connectorCenterOffset = 0.6f;
//      private Texture2D _connectedTexture;
//
//      public void RenderPlane(int x, int y)
//      {
//         // set the parameters for plane drawing and draw it using the basic effect
//         _basicEffect.Texture = _planeTexture;
//         _basicEffect.World = Matrix.RotationX(-MathUtil.PiOverTwo) * Matrix.Translation((x + planeOffset) * 1.2f, 0, (y + planeOffsty) * 1.2f);
//         _plane.Draw(_basicEffect);
//      }
//
//      public void RenderEastConnector(int x, int y)
//      {
//         // set the parameters for plane drawing and draw it using the basic effect
//         _basicEffect.Texture = _connectedTexture;
//         _basicEffect.World = Matrix.RotationX(-MathUtil.PiOverTwo) * Matrix.Translation((x + planeOffset) * 1.2f + 0.6f, 0, (y + planeOffsty) * 1.2f);
//         _hConnector.Draw(_basicEffect);
//      }
//
//      public void RenderSouthConnector(int x, int y)
//      {
//         // set the parameters for plane drawing and draw it using the basic effect
//         _basicEffect.Texture = _connectedTexture;
//         _basicEffect.World = Matrix.RotationX(-MathUtil.PiOverTwo) * Matrix.Translation((x + planeOffset) * 1.2f, 0, (y + planeOffsty) * 1.2f + 0.6f);
//         _vConnector.Draw(_basicEffect);
//      }
//
//      public void ImportGrid(SquareGrid grid, int cx, int cy)
//      {
//         planeOffset = -cx + 0.5f;
//         planeOffsty = -cy + 0.5f;
//         foreach (var cell in grid.Cells) {
//            this.renderables.Add(new SquareCellRenderable(cell, this));
//         }
//      }
   }
}