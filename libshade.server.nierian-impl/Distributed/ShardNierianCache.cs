﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dargon.Distributed;
using Dargon.PortableObjects;
using Shade.Server.SpecializedCache;

namespace Shade.Server.Nierians.Distributed
{
   public class ShardNierianCache
   {
      private readonly string shardId;
      private readonly ICache<NierianKey, NierianEntry> cache;
      private readonly ICountingCache nierianIdCountingCache;
      private readonly ICacheIndex<NierianKey, NierianEntry, ulong> cacheAccountIndex;

      public ShardNierianCache(string shardId, ICache<NierianKey, NierianEntry> cache, ICountingCache nierianIdCountingCache)
      {
         this.shardId = shardId;
         this.cache = cache;
         this.nierianIdCountingCache = nierianIdCountingCache;
         this.cacheAccountIndex = cache.GetIndex<ulong>("ACCOUNT");
      }

      public NierianKey CreateNierian(ulong accountId, string nierianName)
      {
         ulong nierianId = nierianIdCountingCache.Next();
         var result = new NierianKey(shardId, accountId, nierianId);
         if (Cache.Invoke(result, new NierianCreationProcessor(nierianName))) {
            return result;
         } else {
            return null;
         }
      }

      public IEnumerable<NierianEntry> EnumerateNieriansByAccount(ulong accountId)
      {
         return this.cache.FilterEntries(cacheAccountIndex, accountId).Select((e) => e.Value);
      }

      public ICache<NierianKey, NierianEntry> Cache { get { return cache; } }

      public class NierianCreationProcessor : IEntryProcessor<NierianKey, NierianEntry, bool>
      {
         private string nierianName;

         public NierianCreationProcessor(string nierianName)
         {
            this.nierianName = nierianName;
         }

         public bool Process(IEntry<NierianKey, NierianEntry> entry)
         {
            if (entry.IsPresent) {
               return false;
            } else {
               entry.FlagAsDirty();
               entry.Value = new NierianEntry(entry.Key, nierianName);
               return true;
            }
         }

         public void Serialize(IPofWriter writer) { writer.WriteString(0, nierianName); }

         public void Deserialize(IPofReader reader) { nierianName = reader.ReadString(0); }
      }
   }
}