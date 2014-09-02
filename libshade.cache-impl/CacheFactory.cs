using System.Collections.Generic;
using Dargon.Distributed;
using Shade.Server.Accounts.Distributed;

namespace Shade.Server.Cache
{
   public class CacheFactory
   {
      public ICache<TKey, TValue> CreatePersistentCache<TKey, TValue>(string name, IReadOnlyList<ICacheIndex> indices)
      {
         return new InMemoryCache<TKey, TValue>(name, indices);
      }

      public ICacheIndex<TKey, TValue, TProjection> CreatePersistentCacheIndex<TKey, TValue, TProjection>(string name, ICacheProjector<TKey, TValue, TProjection> projector)
      {
         return new InMemoryCache<TKey, TValue>.InMemoryCacheIndex<TKey, TValue, TProjection>(name, projector);
      }
   }
}
