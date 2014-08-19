using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shade.Server.Nierians
{
   public class NierianKeyImpl : NierianKey
   {
      private readonly string shardId;
      private readonly uint accountId;
      private readonly uint nierianId;

      public NierianKeyImpl(string shardId, uint accountId, uint nierianId)
      {
         this.shardId = shardId;
         this.accountId = accountId;
         this.nierianId = nierianId;
      }

      public string ShardId { get { return shardId; } }
      public uint AccountId { get { return accountId; } }
      public uint NierianId { get { return nierianId; } }

      public override bool Equals(object obj)
      {
         var asKey = obj as NierianKey;
         if (asKey == null) {
            return false;
         } else {
            return asKey.ShardId == shardId & asKey.AccountId == accountId && asKey.NierianId == nierianId;
         }
      }

      public override string ToString() { return "[NierianKey " + shardId + "/" + accountId + "/" + nierianId + "]"; }
   }
}
