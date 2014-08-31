using System.Collections.Generic;
using Dargon.Distributed;

namespace Shade.Server.Cache
{
   public class CacheFactory
   {
      public ICache<TKey, TValue> CreatePersistentCache<TKey, TValue>(string name) { 
         return new InMemoryCache<TKey, TValue>(
            name,
            new List<object> {
               new InMemoryCache<TKey, TValue>.InMemoryCacheIndex<TKey, TValue, long>("ACCOUNT_ID", new LambdaProjector())
            }
         ); 
      }
   }
}
