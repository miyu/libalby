using System;

namespace Shade.Client
{
   public interface ClientLevelService
   {
      event LevelChangedHandler LevelChange;

      ShadeLevel GetCurrentLevelModel();
      void SetCurrentLevelModel(ShadeLevel desc);
   }

   public class LevelChangedEventArgs : EventArgs
   {
      public ShadeLevel Previous { get; private set; }
      public ShadeLevel Next { get; private set; }

      public LevelChangedEventArgs(ShadeLevel previous, ShadeLevel next)
      {
         Previous = previous;
         Next = next;
      }
   }

   public delegate void LevelChangedHandler(ClientLevelService service, LevelChangedEventArgs e);
}