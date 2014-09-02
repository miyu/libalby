using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dargon.PortableObjects;

namespace Shade.Server.Nierians.DTOs
{
   public class NierianIdV1 : IPortableObject
   {
      private string shardId;
      private ulong accountId;
      private ulong nierianId;

      public NierianIdV1() { }

      public NierianIdV1(string shardId, ulong accountId, ulong nierianId)
      {
         this.shardId = shardId;
         this.accountId = accountId;
         this.nierianId = nierianId;
      }

      public string ShardId { get { return shardId; } }
      public ulong AccountId { get { return accountId; } }
      public ulong NierianId { get { return nierianId; } }

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

      public override string ToString() { return "[NierianIDV1 { ShardId = " + shardId + ", AccountId = " + accountId + ", NierianId = " + nierianId + " }]"; }
   }
}
