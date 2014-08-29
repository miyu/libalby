using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dargon.Distributed;

namespace Shade.Server.World.Distributed
{
   public class Caches
   {
      public const string WORLD_LOCATION_CACHE_NAME = "world-location-cache";

      private readonly PlatformCacheService cacheService;

      public Caches(PlatformCacheService cacheService)
      {
         this.cacheService = cacheService; 
      }

      public ICache<LocationCacheKey, LocationCacheValue> WorldLocationCache { get { return cacheService.GetKeyValueCache<LocationCacheKey, LocationCacheValue>(WORLD_LOCATION_CACHE_NAME); } }
   }
}
