using System;
using Dargon.PortableObjects;

namespace Shade.Server.Accounts.DataTransferObjects
{
   public class AccountIdV1 : IPortableObject
   {
      private string shardId;
      private ulong accountId;

      public AccountIdV1() { }

      public AccountIdV1(string shardId, ulong accountId)
      {
         this.shardId = shardId;
         this.accountId = accountId;
      }

      public string ShardId { get { return shardId; } }
      public ulong AccountId { get { return accountId; } }

      public void Serialize(IPofWriter writer)
      {
         writer.WriteString(0, shardId);
         writer.WriteU64(1, accountId);
      }

      public void Deserialize(IPofReader reader)
      {
         shardId = reader.ReadString(0);
         accountId = reader.ReadU64(1);
      }
   }
}
