using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dargon.PortableObjects;
using Shade.Server.World.Distributed;

namespace Shade.Server.World
{
   public class WorldPofContext : PofContext
   {
      public WorldPofContext()
      {
         // PofID Range: 10000 - 19999
         this.RegisterPortableObjectType(10000, typeof(LocationCacheKey));
         this.RegisterPortableObjectType(10001, typeof(LocationCacheValue));
         this.RegisterPortableObjectType(10002, typeof(WorldLocation));
         this.RegisterPortableObjectType(10003, typeof(LocationCache.LocationCachePopProcessor));
         this.RegisterPortableObjectType(10004, typeof(LocationCache.LocationCachePushProcessor));
      }
   }
}
