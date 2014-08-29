using Dargon.Distributed;
using Dargon.PortableObjects;
using ItzWarty;

namespace Shade.Server.World.Distributed
{
   public class LocationCache
   {
      private readonly ICache<LocationCacheKey, LocationCacheValue> cache;

      public LocationCache(ICache<LocationCacheKey, LocationCacheValue> cache) { this.cache = cache; }

      public WorldLocation Peek(LocationCacheKey key)
      {
         var value = this.cache.GetValueOrDefault(key);
         if (value == null)
            return null;
         return value.Peek();
      }

      public WorldLocation Pop(LocationCacheKey key) { return cache.Invoke(key, new LocationCachePopProcessor()); }

      public bool Push(LocationCacheKey key, WorldLocation value) { return cache.Invoke(key, new LocationCachePushProcessor(value, false)); }

      public bool ResetAndPush(LocationCacheKey key, WorldLocation value) { return cache.Invoke(key, new LocationCachePushProcessor(value, true)); }

      public ICache<LocationCacheKey, LocationCacheValue> Cache { get { return cache; } }

      public class LocationCachePopProcessor : IEntryProcessor<LocationCacheKey, LocationCacheValue, WorldLocation>
      {
         public WorldLocation Process(IEntry<LocationCacheKey, LocationCacheValue> entry)
         {
            var value = entry.Value;
            if (value.Count == 0)
               return null;
            else {
               entry.FlagAsDirty();
               return value.Pop();
            }
         }
      }

      public class LocationCachePushProcessor : IEntryProcessor<LocationCacheKey, LocationCacheValue, bool>, IPortableObject
      {
         private WorldLocation location;
         private bool reset;

         public LocationCachePushProcessor(WorldLocation location, bool reset)
         {
            this.location = location;
            this.reset = reset;
         }

         public bool Process(IEntry<LocationCacheKey, LocationCacheValue> entry)
         {
            var value = entry.Value;
            if (reset) {
               value.Clear();
            }
            value.Push(location);
            entry.FlagAsDirty();
            return true;
         }

         public void Serialize(IPofWriter writer)
         {
            writer.WriteObject(0, location);
            writer.WriteBoolean(1, reset);
         }

         public void Deserialize(IPofReader reader)
         {
            location = reader.ReadObject<WorldLocation>(0);
            reset = reader.ReadBoolean(1);
         }
      } 
   }
}
