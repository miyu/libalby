using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dargon.Distributed;
using Shade.Server.SpecializedCache;

namespace Shade.Server.Accounts.Distributed
{
   public class Caches
   {
      private readonly ICache<AccountKey, AccountEntry> accountCache;
      private readonly ICountingCache accountIdCountingCache;

      public const string ACCOUNT_CACHE_NAME = "account-cache";
      public const string ACCOUNT_ID_COUNTER_CACHE_NAME = "account-id-counter-cache";

      public Caches(string shardId, PlatformCacheService platformCacheService, SpecializedCacheService specializedCacheService)
      {
         this.accountCache = platformCacheService.GetKeyValueCache<AccountKey, AccountEntry>(shardId, ACCOUNT_CACHE_NAME);
         this.accountIdCountingCache = specializedCacheService.GetCountingCache(shardId, ACCOUNT_ID_COUNTER_CACHE_NAME);
      }

      public ICache<AccountKey, AccountEntry> AccountCache { get { return accountCache; } }
      public ICountingCache AccountIdCountingCache { get { return accountIdCountingCache; } }
   }
}
