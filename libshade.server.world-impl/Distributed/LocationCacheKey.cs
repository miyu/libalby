using Dargon.PortableObjects;
using System;

namespace Shade.Server.World.Distributed
{
   public class LocationCacheKey : IPortableObject
   {
      private string shardId;
      private uint accountId;
      private uint nierianId;

      public LocationCacheKey() { }

      public LocationCacheKey(string shardId, uint accountId, uint nierianId)
      {
         this.shardId = shardId;
         this.accountId = accountId;
         this.nierianId = nierianId;
      }

      public void Serialize(IPofWriter writer)
      {
         writer.WriteString(0, shardId);
         writer.WriteU32(1, accountId);
         writer.WriteU32(2, nierianId);
      }

      public void Deserialize(IPofReader reader)
      {
         shardId = reader.ReadString(0);
         accountId = reader.ReadU32(1);
         nierianId = reader.ReadU32(2);
      }
   }
}
