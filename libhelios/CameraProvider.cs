using SharpDX;
using SharpDX.Toolkit;

namespace Shade.Helios
{
   internal sealed class CameraProvider : GameSystem, ICameraService
   {
      private Matrix _view;
      private Matrix _projection;

      public CameraProvider(Game game)
         : base(game)
      {
         Enabled = true;
         game.GameSystems.Add(this);
         game.Services.AddService(typeof(ICameraService), this);
      }

      public Matrix View { get { return _view; } }
      public Matrix Projection { get { return _projection; } }

      public override void Update(GameTime gameTime)
      {
         base.Update(gameTime);

         var viewRotationAngle = 0; // MathUtil.DegreesToRadians(-10.0f); //(float)(gameTime.TotalGameTime.TotalSeconds * 0.2f);
         var eyePosition = Vector3.Transform(new Vector3(0, 10f, 4f), Quaternion.RotationAxis(Vector3.UnitY, viewRotationAngle));

         _view = Matrix.LookAtRH(eyePosition, new Vector3(0, 0, 0), Vector3.UnitY);
         _projection = Matrix.PerspectiveFovRH(MathUtil.DegreesToRadians(60.0f), (float)GraphicsDevice.BackBuffer.Width / GraphicsDevice.BackBuffer.Height, 0.1f, 200.0f);
      }
   }
}