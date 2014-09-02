using System;
using System.Collections.Generic;
using System.Linq;
using Dargon.Distributed;
using Dargon.PortableObjects;
using ItzWarty;
using Shade.Server.Accounts;
using Shade.Server.Accounts.Distributed;
using Shade.Server.Nierians;
using Shade.Server.Nierians.Distributed;
using Shade.Server.SpecializedCache;
using Shade.Server.World;
using Shade.Server.World.Distributed;

namespace Shade.Server.Cache
{
    public class CacheServiceImpl : PlatformCacheService
    {
       private readonly PofContext globalPofContext = new PofContext();
       private readonly Dictionary<string, ICache> cachesByName = new Dictionary<string, ICache>(); 
       private readonly CacheFactory cacheFactory = new CacheFactory();
       private readonly PlatformConfiguration platformConfiguration;
       private readonly string[] shardIds;

       public CacheServiceImpl(ShadeServiceLocator serviceLocator, PlatformConfiguration platformConfiguration)
       {
          serviceLocator.RegisterService(typeof(PlatformCacheService), this);

          this.platformConfiguration = platformConfiguration;
          this.shardIds = platformConfiguration.ShardConfigurations.Select(c => c.ShardId).ToArray();

          InitializeSpecializedCaches();  // 0 - 999, 1000 - 9999 remaining
          InitializeWorldCaches();        // 10000 - 19999
          InitializeAccountCaches();      // 20000 - 29999
          InitializeNierianCaches();      // 30000 - 39999
       }

       private void InitializeSpecializedCaches()
       {
          globalPofContext.MergeContext(new SpecializedCachePofContext());

          foreach (var shardId in shardIds) {
             RegisterPersistentNamedCache<string, ulong>(BuildCacheName(shardId, SpecializedCache.Distributed.Caches.COUNTING_CACHE_NAME));
          }
       }

       private void InitializeWorldCaches()
       {
          globalPofContext.MergeContext(new WorldPofContext());

          foreach (var shardId in shardIds) {
             RegisterPersistentNamedCache<LocationCacheKey, LocationCacheValue>(BuildCacheName(shardId, World.Distributed.Caches.WORLD_LOCATION_CACHE_NAME));
          }
       }

       private void InitializeAccountCaches()
       {
          globalPofContext.MergeContext(new AccountPofContext());

          foreach (var shardId in shardIds) {
             RegisterPersistentNamedCache<AccountKey, AccountEntry>(BuildCacheName(shardId, Accounts.Distributed.Caches.ACCOUNT_CACHE_NAME));
          }
       }

       private void InitializeNierianCaches()
       {
          globalPofContext.MergeContext(new NierianPofContext());
          
          foreach (var shardId in shardIds) {
             RegisterPersistentNamedCache<NierianKey, NierianEntry>(
                BuildCacheName(shardId, Nierians.Distributed.Caches.NIERIAN_CACHE_NAME),
                new List<ICacheIndex> {
                   ConstructPersistentNamedCacheIndex(Nierians.Distributed.Caches.NIERIAN_CACHE_ACCOUNT_ID_INDEX_NAME, new AccountIdFromNierianEntryProjector())
                }
             );
          }
       }

       private void RegisterPersistentNamedCache<TKey, TValue>(string name, IReadOnlyList<ICacheIndex> indices = null)
       {
          Console.WriteLine("Register Persistent Cache " + name);
          cachesByName.Add(name, cacheFactory.CreatePersistentCache<TKey, TValue>(name, indices));
       }

       private ICacheIndex<TKey, TValue, TProjection> ConstructPersistentNamedCacheIndex<TKey, TValue, TProjection>(string name, ICacheProjector<TKey, TValue, TProjection> projector)
       {
          return cacheFactory.CreatePersistentCacheIndex(name, projector);
       }

       public IPofContext GlobalPofContext { get { return globalPofContext; } }

       public ICache<TKey, TValue> GetKeyValueCache<TKey, TValue>(string name) { return (ICache<TKey, TValue>)cachesByName.GetValueOrDefault(name); }
       public ICache<TKey, TValue> GetKeyValueCache<TKey, TValue>(string shardId, string name) { return GetKeyValueCache<TKey, TValue>(BuildCacheName(shardId, name)); }

       private string BuildCacheName(string shardId, string cacheName) { return shardId + "/" + cacheName; }
    }
}
