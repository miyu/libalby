using Dargon.PortableObjects;
using Shade.Server.Nierians.DTOs;

namespace Shade.Server.Nierians.Distributed
{
   public class NierianKey : IPortableObject
   {
      private string shardId;
      private ulong accountId;
      private ulong nierianId;

      public NierianKey(string shardId, ulong accountId, ulong nierianId)
      {
         this.shardId = shardId;
         this.accountId = accountId;
         this.nierianId = nierianId;
      }

      public string ShardId { get { return shardId; } }
      public ulong AccountId { get { return accountId; } }
      public ulong NierianId { get { return nierianId; } }

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

      public NierianIdV1 ToNierianIdV1() { return new NierianIdV1(shardId, accountId, nierianId); }

      public void Serialize(IPofWriter writer)
      {
         writer.WriteString(0, shardId);
         writer.WriteU64(1, accountId);
         writer.WriteU64(2, nierianId);
      }

      public void Deserialize(IPofReader reader)
      {
         shardId = reader.ReadString(0);
         accountId = reader.ReadU64(1);
         nierianId = reader.ReadU64(2);
      }
   }
}
