using System.Collections.Generic;
using System.Diagnostics;
using ItzWarty;
using ItzWarty.Collections;
using Shade.Server.Accounts;

namespace Shade.Server.Nierians
{
   public class ShardNierianServiceImpl : ShardNierianService
   {
      private readonly string shardId;
      private readonly Dictionary<uint, Nierian> nieriansByNierianId = new Dictionary<uint, Nierian>();
      private readonly MultiValueDictionary<string, Nierian> nieriansByAccountKey = new MultiValueDictionary<string, Nierian>();

      private uint nierianIdCounter = 0;

      public ShardNierianServiceImpl(string shardId) { this.shardId = shardId; }

      public NierianKey CreateNierian(AccountKey accountKey, string nierianName)
      {
         var key = BuildKey(accountKey);
         uint nierianId = nierianIdCounter++;
         var nierianKey = new NierianKeyImpl(shardId, accountKey.AccountId, nierianId);
         var nierian = new NierianImpl(nierianKey, nierianName);
         nieriansByNierianId.Add(nierianId, nierian);
         nieriansByAccountKey.Add(key, nierian);
         return nierianKey;
      }

      public void SetNierianName(Nierian nierianId, string name)
      {
         //nieriansByNierianId.GetValueOrDefault()
      }

      private string BuildKey(AccountKey accountKey) { return accountKey.ShardId + "/" + accountKey.AccountId; }
   }
}