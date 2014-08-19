using System;

namespace Shade.Client
{
   public class ClientLevelServiceImpl : ClientLevelService
   {
      private readonly IShadeClient client;
      private ShadeLevel currentShadeLevel;

      public event LevelChangedHandler LevelChange;

      public ClientLevelServiceImpl(IShadeClient client)
      {
         this.client = client;
         this.client.Services.AddService(typeof(ClientLevelService), this);
      }

      public ShadeLevel GetCurrentLevelModel() { return currentShadeLevel; }

      public void SetCurrentLevelModel(ShadeLevel desc)
      {
         var previous = currentShadeLevel;

         currentShadeLevel = desc;

         var capture = LevelChange;
         if (capture != null)
            capture(this, new LevelChangedEventArgs(previous, desc));
      }
   }
}
