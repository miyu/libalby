using Dargon.PortableObjects;
using Shade.Server.Accounts;
using Shade.Server.Nierians.Distributed;

namespace Shade.Server.Nierians
{
   public class NierianEntry : IPortableObject
   {
      private readonly NierianKey nierianKey;
      private string name;

      public NierianEntry(NierianKey niarianKey, string nierianName)
      {
         this.nierianKey = niarianKey;
         this.name = nierianName;
      }

      public NierianKey Key { get { return nierianKey; } }
      public string Name { get { return name; } }

      public void Serialize(IPofWriter writer) { throw new System.NotImplementedException(); }

      public void Deserialize(IPofReader reader) { throw new System.NotImplementedException(); }
   }
}