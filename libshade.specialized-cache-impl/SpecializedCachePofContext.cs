using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dargon.PortableObjects;
using Shade.Server.SpecializedCache.Distributed;

namespace Shade.Server.SpecializedCache
{
   public class SpecializedCachePofContext : PofContext
   {
      public SpecializedCachePofContext()
      {
         // range 0 - 999
         this.RegisterPortableObjectType(0, typeof(CountingCache.PostIncrementProcessor));
      }
   }
}
