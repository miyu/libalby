using System;
using Shade.Server.LevelHostManager;
using Shade.Server.Nierians;

namespace Shade.Server.World
{
   public class WorldServiceImpl : WorldService
   {
      private readonly ShadeServiceLocator serviceLocator;
      private readonly PlatformConfiguration platformConfiguration;
      private readonly NierianService nierianService;
      private readonly WorldLevelHostManagerService worldLevelHostManagerService;

      public WorldServiceImpl(ShadeServiceLocator serviceLocator, PlatformConfiguration platformConfiguration)
      {
         this.serviceLocator = serviceLocator;
         this.nierianService = serviceLocator.NierianService;
         this.platformConfiguration = platformConfiguration;
      }

      public WorldLoginResult Enter(NierianKey nierianKey)
      {
         // CreateOrGetNierianWorldInformation(nierianKey);
         //var sessionToken = Guid.NewGuid().ToString();
         //nierianService.GetNierian(nierianKey);
         throw new NotImplementedException();
      }

      private void SanitizeNierianPosition(NierianKey nierianKey)
      {
  
      }

      // private WorldState CreateOrGetNierianWorldInformation(NierianKey nierianKey) { }

      public void Leave(NierianKey key) { throw new System.NotImplementedException(); }
   }
}
