using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItzWarty.Services;
using Shade.Server.SpecializedCache.Distributed;

namespace Shade.Server.SpecializedCache
{
   public class SpecializedCacheServiceImpl : SpecializedCacheService
   {
      private readonly IServiceLocator serviceLocator;
      private readonly PlatformConfiguration platformConfiguration;
      private readonly PlatformCacheService platformCacheService;
      private readonly Dictionary<string, Caches> cachesByShardId = new Dictionary<string, Caches>(); 

      public SpecializedCacheServiceImpl(IServiceLocator serviceLocator, PlatformConfiguration platformConfiguration, PlatformCacheService platformCacheService)
      {
         this.serviceLocator = serviceLocator;
         this.platformConfiguration = platformConfiguration;
         this.platformCacheService = platformCacheService;

         foreach (var shardId in platformConfiguration.ShardConfigurations.Select(c => c.ShardId)) {
            cachesByShardId.Add(shardId, new Caches(shardId, platformCacheService));
         }
      }

      public ICountingCache GetCountingCache(string shardId, string name) { return new CountingCache(name, cachesByShardId[shardId].CountingCache); }
   }
}
