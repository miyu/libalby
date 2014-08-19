namespace Shade.Server.Accounts
{
   public class AccountKeyImpl : AccountKey
   {
      private string shardId;
      private uint accountId;

      public AccountKeyImpl(string shardId, uint accountId)
      {
         this.shardId = shardId;
         this.accountId = accountId;
      }

      public string ShardId { get { return shardId; } }
      public uint AccountId { get { return accountId; } }

      public override string ToString() { return "[AccountKey " + shardId + "/" + accountId + "]"; }
   }
}