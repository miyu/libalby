using Dargon.Distributed;
using Shade.Server.SpecializedCache;

namespace Shade.Server.Nierians.Distributed
{
   public class Caches
   {
      private readonly ICache<NierianKey, NierianEntry> nierianCache;
      private readonly ICountingCache nierianIdCountingCache;

      public const string NIERIAN_CACHE_NAME = "nierian-cache";
      public const string NIERIAN_ID_COUNTER_CACHE_NAME = "nierian-id-counter-cache";

      public Caches(string shardId, PlatformCacheService platformCacheService, SpecializedCacheService specializedCacheService)
      {
         this.nierianCache = platformCacheService.GetKeyValueCache<NierianKey, NierianEntry>(shardId, NIERIAN_CACHE_NAME);
         this.nierianIdCountingCache = specializedCacheService.GetCountingCache(shardId, NIERIAN_ID_COUNTER_CACHE_NAME);
      }

      public ICache<NierianKey, NierianEntry> AccountCache { get { return nierianCache; } }
      public ICountingCache AccountIdCountingCache { get { return nierianIdCountingCache; } }
   }
}
