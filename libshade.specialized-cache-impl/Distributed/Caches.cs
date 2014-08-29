using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dargon.Distributed;

namespace Shade.Server.SpecializedCache.Distributed
{
   public class Caches
   {
      public const string COUNTING_CACHE_NAME = "counting-cache";

      private readonly PlatformCacheService platformCacheService;
      private readonly ICache<string, ulong> countingCache;

      public Caches(string shardId, PlatformCacheService platformCacheService)
      {
         this.platformCacheService = platformCacheService;
         this.countingCache = platformCacheService.GetKeyValueCache<string, ulong>(shardId, COUNTING_CACHE_NAME);
      }

      public ICache<string, ulong> CountingCache { get { return countingCache; } }
   }
}
