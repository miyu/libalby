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

      private readonly string shardId;
      private readonly PlatformCacheService cacheService;
      private readonly ICache<LocationCacheKey, LocationCacheValue> worldLocationCache;

      public Caches(string shardId, PlatformCacheService cacheService)
      {
         this.shardId = shardId;
         this.cacheService = cacheService;
         this.worldLocationCache = cacheService.GetKeyValueCache<LocationCacheKey, LocationCacheValue>(shardId, WORLD_LOCATION_CACHE_NAME);
      }

      public ICache<LocationCacheKey, LocationCacheValue> WorldLocationCache { get { return worldLocationCache; } }
   }
}
