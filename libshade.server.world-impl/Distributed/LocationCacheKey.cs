using Dargon.PortableObjects;
using System;

namespace Shade.Server.World.Distributed
{
   public class LocationCacheKey : IPortableObject
   {
      private string shardId;
      private ulong accountId;
      private ulong nierianId;

      public LocationCacheKey() { }

      public LocationCacheKey(string shardId, ulong accountId, ulong nierianId)
      {
         this.shardId = shardId;
         this.accountId = accountId;
         this.nierianId = nierianId;
      }

      public void Serialize(IPofWriter writer)
      {
         writer.WriteString(0, shardId);
         writer.WriteU64(1, accountId);
         writer.WriteU64(2, nierianId);
      }

      public void Deserialize(IPofReader reader)
      {
         shardId = reader.ReadString(0);
         accountId = reader.ReadU64(1);
         nierianId = reader.ReadU64(2);
      }
   }
}
