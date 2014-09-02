using Dargon.PortableObjects;

namespace Shade.Server.World
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

      public uint LevelId { get { return levelId; } }
      public float X { get { return x; } }
      public float Y { get { return y; } }
      public float Z { get { return z; } }

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

      public WorldLocationV1 ToWorldLocationV1() { return new WorldLocationV1(levelId, x, y, z); }
   }
}
