using System.Collections.Generic;

namespace Shade.Server.Accounts
{
   public class ShardAccountServiceImpl : ShardAccountService
   {
      private readonly string shardId;
      private readonly Dictionary<string, Account> accountsByUsername = new Dictionary<string, Account>();
      private readonly Dictionary<uint, Account> accountsById = new Dictionary<uint, Account>();

      private uint accountIdCounter = 0;

      public ShardAccountServiceImpl(string shardId) { this.shardId = shardId; }

      public AccountKey CreateAccount(string username)
      {
         var accountId = accountIdCounter++;
         var accountKey = new AccountKeyImpl(shardId, accountId);
         var account = new AccountImpl(accountKey, username);

         accountsById.Add(accountId, account);
         accountsByUsername.Add(username, account);

         return accountKey;
      }
   }
}