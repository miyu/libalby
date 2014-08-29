using Dargon.Distributed;

namespace Shade.Server.SpecializedCache.Distributed
{
   public class CountingCache : ICountingCache
   {
      private readonly string name;
      private readonly ICache<string, ulong> countCache;

      public CountingCache(string name, ICache<string, ulong> countCache)
      {
         this.name = name;
         this.countCache = countCache;
      }

      public string Name { get { return name; } }

      public ulong Next() { return countCache.Invoke(name, new PostIncrementProcessor()); }

      public class PostIncrementProcessor : IEntryProcessor<string, ulong, ulong>
      {
         public ulong Process(IEntry<string, ulong> entry)
         {
            entry.FlagAsDirty();
            if (!entry.IsPresent) {
               return entry.Value = 0;
            } else {
               return entry.Value++;
            }
         }
      }
   }
}