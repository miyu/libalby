using Dargon.PortableObjects;
using Shade.Server.Accounts.DataTransferObjects;

namespace Shade.Server.Accounts.Distributed
{
   public class AccountKey : IPortableObject
   {
      private string shardId;
      private ulong accountId;

      public AccountKey(string shardId, ulong accountId)
      {
         this.shardId = shardId;
         this.accountId = accountId;
      }

      public string ShardId { get { return shardId; } }
      public ulong AccountId { get { return accountId; } }

      public override string ToString() { return "[AccountKey " + shardId + "/" + accountId + "]"; }

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

      public AccountIdV1 ToAccountIdV1() { return new AccountIdV1(shardId, accountId); }
   }
}
