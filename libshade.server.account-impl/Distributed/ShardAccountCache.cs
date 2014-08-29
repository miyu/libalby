using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dargon.Distributed;
using Dargon.PortableObjects;
using Shade.Server.SpecializedCache;

namespace Shade.Server.Accounts.Distributed
{
   public class ShardAccountCache
   {
      private readonly string shardId;
      private readonly ICache<AccountKey, AccountEntry> cache;
      private readonly ICountingCache accountIdCountingCache;

      public ShardAccountCache(string shardId, ICache<AccountKey, AccountEntry> cache, ICountingCache accountIdCountingCache)
      {
         this.shardId = shardId;
         this.cache = cache;
         this.accountIdCountingCache = accountIdCountingCache;
      }

      public AccountKey CreateAccount(string username)
      {
         ulong accountId = accountIdCountingCache.Next();
         var result = new AccountKey(shardId, accountId);
         if (Cache.Invoke(new AccountKey(shardId, accountId), new AccountCreationProcessor(username))) {
            return result;
         } else {
            return null;
         }
      }

      public ICache<AccountKey, AccountEntry> Cache { get { return cache; } }

      public class AccountCreationProcessor : IEntryProcessor<AccountKey, AccountEntry, bool>, IPortableObject
      {
         private string username;

         public AccountCreationProcessor(string username)
         {
            this.username = username;
         }

         public bool Process(IEntry<AccountKey, AccountEntry> entry)
         {
            if (entry.IsPresent) {
               return false;
            } else {
               entry.FlagAsDirty();
               entry.Value = new AccountEntry(entry.Key, username);
               return true;
            }
         }

         public void Serialize(IPofWriter writer) { writer.WriteString(0, username); }

         public void Deserialize(IPofReader reader) { username = reader.ReadString(0); }
      }
   }
}
