using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dargon.PortableObjects;

namespace Shade.Server.World.Distributed
{
   public class LocationCacheValue : IPortableObject
   {
      private readonly Stack<WorldLocation> locationStack = new Stack<WorldLocation>();

      public LocationCacheValue() { }
      public LocationCacheValue(WorldLocation location) { locationStack.Push(location); }

      public int Count { get { return locationStack.Count; } }
      public WorldLocation Peek() { return locationStack.Count == 0 ? null : locationStack.Peek(); }
      public WorldLocation Pop() { return locationStack.Pop(); }
      public void Push(WorldLocation value) { locationStack.Push(value); }
      public void Clear() { locationStack.Clear(); }

      public void Serialize(IPofWriter writer)
      {
         writer.WriteArray(0, locationStack.ToArray());
      }

      public void Deserialize(IPofReader reader)
      {
         locationStack.Clear();
         foreach (var location in reader.ReadArray<WorldLocation>(0)) {
            locationStack.Push(location);
         }
      }
   }
}
