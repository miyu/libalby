using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shade.Server.SpecializedCache
{
   public interface SpecializedCacheService
   {
      ICountingCache GetCountingCache(string shardId, string name);
   }
}
