using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shade.Server.Accounts;
using ItzWarty;

namespace Shade.Server.Nierians
{
   public class NierianServiceImpl : NierianService
   {
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

      public NierianKey CreateNierian(AccountKey accountKey, string nierianName)
      {
         var shardNierianService = shardNierianServicesByShardId.GetValueOrDefault(accountKey.ShardId);

         NierianKey nierianKey = null;
         if (shardNierianService != null) {
            nierianKey = shardNierianService.CreateNierian(accountKey, nierianName);
         }
         return nierianKey;
      }

      public void SetNierianName(Nierian nierian, string name)
      {
         var shardNierianService = shardNierianServicesByShardId.GetValueOrDefault(nierian.Key.ShardId);
         if (shardNierianService != null) {
            shardNierianService.SetNierianName(nierian, name);
         }
      }

      public IReadOnlyCollection<Nierian> GetNieriansByAccount(AccountKey accountKey) { throw new NotImplementedException(); }
   }
}