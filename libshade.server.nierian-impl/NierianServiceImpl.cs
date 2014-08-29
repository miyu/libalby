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

namespace Shade.Server.Nierians
{
   public class NierianServiceImpl : NierianService
   {
      private static Logger logger = LogManager.GetCurrentClassLogger();

      private Dictionary<string, ShardNierianServiceImpl> shardNierianServicesByShardId = new Dictionary<string, ShardNierianServiceImpl>();

      public NierianServiceImpl(ShadeServiceLocator shadeServiceLocator, PlatformConfiguration platformConfiguration)
      {
         shadeServiceLocator.RegisterService(typeof(NierianService), this);

         foreach (var shardConfiguration in platformConfiguration.ShardConfigurations) {
            InitializeShard(shardConfiguration);
         }
      }

      private void InitializeShard(ShardConfiguration shardConfiguration)
      {
         string shardId = shardConfiguration.ShardId;
         var shardNierianSerice = new ShardNierianServiceImpl(shardId);
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

      public void SetNierianName(NierianEntry nierianEntry, string name)
      {
         var shardNierianService = shardNierianServicesByShardId.GetValueOrDefault(nierianEntry.Key.ShardId);
         if (shardNierianService != null) {
            shardNierianService.SetNierianName(nierianEntry, name);
         }
      }

      public IEnumerable<NierianEntry> EnumerateNieriansByAccount(string shardId, ulong accountId)
      {
         var shardNierianService = shardNierianServicesByShardId.GetValueOrDefault(shardId);
         if (shardNierianService != null)
            return shardNierianService.EnumerateNieriansByAccount(accountId);
         return Enumerable.Empty<NierianEntry>();
      }
   }
}