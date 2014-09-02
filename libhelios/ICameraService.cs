using SharpDX;

namespace Shade.Helios
{
   public interface ICameraService
   {
      Matrix View { get; }
      Matrix Projection { get; }
   }
}