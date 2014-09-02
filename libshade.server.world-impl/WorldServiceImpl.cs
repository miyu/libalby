using System;
using System.Collections.Generic;
using System.Linq;
using ItzWarty;
using Shade.Server.Dungeons;
using Shade.Server.LevelHostManager;
using Shade.Server.Nierians;
using Shade.Server.World.Distributed;

namespace Shade.Server.World
{
   public class WorldServiceImpl : WorldService
   {
      private readonly ShadeServiceLocator serviceLocator;
      private readonly PlatformConfiguration platformConfiguration;
      private readonly PlatformCacheService platformCacheService;
      private readonly NierianService nierianService;
      private readonly DungeonService dungeonService;

      private readonly Dictionary<string, ShardWorldServiceImpl> shardWorldServicesByShardId = new Dictionary<string, ShardWorldServiceImpl>();
      
      public WorldServiceImpl(ShadeServiceLocator serviceLocator, PlatformConfiguration platformConfiguration, PlatformCacheService platformCacheService, NierianService nierianService, DungeonService dungeonService)
      {
         this.serviceLocator = serviceLocator;
         this.platformConfiguration = platformConfiguration;
         this.platformCacheService = platformCacheService;
         this.nierianService = nierianService;
         this.dungeonService = dungeonService;

         foreach (var shardId in platformConfiguration.ShardConfigurations.Select(c => c.ShardId)) {
            InitializeShard(shardId);
         }
      }

      private void InitializeShard(string shardId)
      {
         shardWorldServicesByShardId.Add(shardId, new ShardWorldServiceImpl(shardId, platformCacheService, nierianService, dungeonService));
      }

      public WorldLoginResult Enter(string shardId, ulong accountId, ulong nierianId)
      {
         var shardWorldService = shardWorldServicesByShardId.GetValueOrDefault(shardId);

         WorldLoginResult result = null;
         if (shardWorldService != null) {
            result = shardWorldService.Enter(accountId, nierianId);
         }
         return result;
      }

      public void Leave(string shardId, ulong accountId, ulong nierianId)
      {
         throw new NotImplementedException();
      }
   }
}
