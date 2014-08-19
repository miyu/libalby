using SharpDX;

namespace Shade.Helios
{
   internal interface ICameraService
   {
      Matrix View { get; }
      Matrix Projection { get; }
   }
}