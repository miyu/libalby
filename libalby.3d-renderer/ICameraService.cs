using SharpDX;

namespace Alby.Gui
{
   internal interface ICameraService
   {
      Matrix View { get; }
      Matrix Projection { get; }
   }
}