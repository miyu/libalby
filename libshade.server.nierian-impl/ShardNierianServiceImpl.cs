using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ItzWarty;
using ItzWarty.Collections;
using Shade.Server.Accounts;
using Shade.Server.Nierians.Distributed;
using Shade.Server.Nierians.DTOs;
using Shade.Server.SpecializedCache;

namespace Shade.Server.Nierians
{
   public class ShardNierianServiceImpl : ShardNierianService
   {
      private readonly string shardId;
      private readonly PlatformCacheService platformCacheService;
      private readonly SpecializedCacheService specializedCacheService;

      private readonly Caches caches;
      private readonly ShardNierianCache shardNierianCache;

      public ShardNierianServiceImpl(string shardId, PlatformCacheService platformCacheService, SpecializedCacheService specializedCacheService)
      {
         this.shardId = shardId;
         this.platformCacheService = platformCacheService;
         this.specializedCacheService = specializedCacheService;

         this.caches = new Caches(shardId, platformCacheService, specializedCacheService);
         this.shardNierianCache = new ShardNierianCache(shardId, caches.AccountCache, caches.AccountIdCountingCache);
      }

      public NierianIdV1 CreateNierian(ulong accountId, string nierianName) { return shardNierianCache.CreateNierian(accountId, nierianName).ToNierianIdV1(); }

      public IEnumerable<NierianIdV1> EnumerateNieriansByAccount(ulong accountKey)
      {
         return shardNierianCache.EnumerateNieriansByAccount(accountKey).Select(n => n.Key.ToNierianIdV1());
      }

      public void SetNierianName(ulong accountId, ulong nierianId, string name) { shardNierianCache.SetNierianName(accountId, nierianId, name); }

      private string BuildKey(ulong accountId) { return shardId + "/" + accountId; }
   }
}