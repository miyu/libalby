using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using Shade.Server.Accounts;
using ItzWarty;
using Shade.Server.Accounts.DataTransferObjects;
using Shade.Server.Nierians.DTOs;
using Shade.Server.SpecializedCache;

namespace Shade.Server.Nierians
{
   public class NierianServiceImpl : NierianService
   {
      private static Logger logger = LogManager.GetCurrentClassLogger();

      private readonly PlatformConfiguration platformConfiguration;
      private readonly PlatformCacheService platformCacheService;
      private readonly SpecializedCacheService specializedCacheService;

      private readonly Dictionary<string, ShardNierianServiceImpl> shardNierianServicesByShardId = new Dictionary<string, ShardNierianServiceImpl>();

      public NierianServiceImpl(ShadeServiceLocator shadeServiceLocator, PlatformConfiguration platformConfiguration, PlatformCacheService platformCacheService, SpecializedCacheService specializedCacheService)
      {
         shadeServiceLocator.RegisterService(typeof(NierianService), this);
         this.platformConfiguration = platformConfiguration;
         this.platformCacheService = platformCacheService;
         this.specializedCacheService = specializedCacheService;

         foreach (var shardConfiguration in platformConfiguration.ShardConfigurations) {
            InitializeShard(shardConfiguration);
         }
      }

      private void InitializeShard(ShardConfiguration shardConfiguration)
      {
         string shardId = shardConfiguration.ShardId;
         var shardNierianSerice = new ShardNierianServiceImpl(shardId, platformCacheService, specializedCacheService);
         shardNierianServicesByShardId.Add(shardId, shardNierianSerice);
      }

      public NierianIdV1 CreateNierian(string shardId, ulong accountId, string nierianName)
      {
         var shardNierianService = shardNierianServicesByShardId.GetValueOrDefault(shardId);

         NierianIdV1 nierianKey = null;
         if (shardNierianService != null) {
            nierianKey = shardNierianService.CreateNierian(accountId, nierianName);
         }

         logger.Info("Create NierianEntry {0} for account {1} => key {2}", nierianName, shardId + "/" + accountId, nierianKey);
         return nierianKey;
      }

      public void SetNierianName(string shardId, ulong accountId, ulong nierianId, string name)
      {
         var shardNierianService = shardNierianServicesByShardId.GetValueOrDefault(shardId);
         if (shardNierianService != null) {
            shardNierianService.SetNierianName(accountId, nierianId, name);
         }
      }

      public IEnumerable<NierianIdV1> EnumerateNieriansByAccount(string shardId, ulong accountId)
      {
         var shardNierianService = shardNierianServicesByShardId.GetValueOrDefault(shardId);
         if (shardNierianService != null)
            return shardNierianService.EnumerateNieriansByAccount(accountId);
         return Enumerable.Empty<NierianIdV1>();
      }
   }
}