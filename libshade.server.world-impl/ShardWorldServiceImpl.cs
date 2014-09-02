using System;
using Shade.Server.Dungeons;
using Shade.Server.Nierians;
using Shade.Server.World.Distributed;

namespace Shade.Server.World
{
   public class ShardWorldServiceImpl : ShardWorldService
   {
      private readonly string shardId;

      private readonly Caches caches;
      private readonly LocationCache locationCache;

      public ShardWorldServiceImpl(string shardId, PlatformCacheService platformCacheService, NierianService nierianService, DungeonService dungeonService)
      {
         this.shardId = shardId; 
         this.caches = new Caches(shardId, platformCacheService);
         this.locationCache = new LocationCache(caches.WorldLocationCache);
      }

      public WorldLoginResult Enter(ulong accountId, ulong nierianId)
      {
         var sessionToken = Guid.NewGuid().ToString();
         var location = locationCache.Peek(shardId, accountId, nierianId);
         return new WorldLoginResult(sessionToken, location.ToWorldLocationV1());
      }

      public void Leave(ulong accountId, ulong nierianId) { throw new System.NotImplementedException(); }
   }
}