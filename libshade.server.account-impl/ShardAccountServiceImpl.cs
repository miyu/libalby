using System.Collections.Generic;
using Dargon.Distributed;
using Shade.Server.Accounts.DataTransferObjects;
using Shade.Server.Accounts.Distributed;
using Shade.Server.SpecializedCache;

namespace Shade.Server.Accounts
{
   public class ShardAccountServiceImpl : ShardAccountService
   {
      private readonly string shardId;
      private readonly PlatformCacheService platformCacheService;
      private readonly SpecializedCacheService specializedCacheService;

      private readonly Caches caches;
      private readonly ShardAccountCache shardAccountCache;
      
      public ShardAccountServiceImpl(string shardId, PlatformCacheService platformCacheService, SpecializedCacheService specializedCacheService)
      {
         this.shardId = shardId;
         this.platformCacheService = platformCacheService;
         this.specializedCacheService = specializedCacheService;

         this.caches = new Caches(shardId, platformCacheService, specializedCacheService);
         this.shardAccountCache = new ShardAccountCache(shardId, caches.AccountCache, caches.AccountIdCountingCache);
      }

      public AccountIdV1 CreateAccount(string username)
      {
         return this.shardAccountCache.CreateAccount(username).ToAccountIdV1();
      }
   }
}