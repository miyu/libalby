using Dargon.Distributed;
using Dargon.PortableObjects;

namespace Shade.Server
{
    public interface PlatformCacheService
    {
       IPofContext GlobalPofContext { get; }
       ICache<TKey, TValue> GetKeyValueCache<TKey, TValue>(string name);
       ICache<TKey, TValue> GetKeyValueCache<TKey, TValue>(string shardId, string name);
    }
}
