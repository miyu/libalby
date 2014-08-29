using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dargon.PortableObjects;

namespace Shade.Server.World.Distributed
{
   public class WorldLocation : IPortableObject
   {
      private uint levelId;
      private float x;
      private float y;
      private float z;

      public WorldLocation() { }

      public WorldLocation(uint levelId, float x, float y, float z)
      {
         this.levelId = levelId;
         this.x = x;
         this.y = y;
         this.z = z;
      }

      public void Serialize(IPofWriter writer)
      {
         writer.WriteU32(0, levelId);
         writer.WriteFloat(1, x);
         writer.WriteFloat(2, y);
         writer.WriteFloat(3, z);
      }

      public void Deserialize(IPofReader reader)
      {
         levelId = reader.ReadU32(0);
         x = reader.ReadFloat(1);
         y = reader.ReadFloat(2);
         z = reader.ReadFloat(3);
      }
   }
}
