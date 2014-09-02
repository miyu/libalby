using Dargon.PortableObjects;

namespace Shade.Server.Nierians.Distributed
{
   public class NierianEntry : IPortableObject
   {
      private NierianKey nierianKey;
      private string name;

      public NierianEntry(NierianKey niarianKey, string nierianName)
      {
         this.nierianKey = niarianKey;
         this.name = nierianName;
      }

      public NierianKey Key { get { return nierianKey; } }
      public string Name { get { return name; } set { name = value; } }

      public void Serialize(IPofWriter writer)
      {
         writer.WriteObject(0, nierianKey);
         writer.WriteString(1, name);
      }

      public void Deserialize(IPofReader reader)
      {
         nierianKey = reader.ReadObject<NierianKey>(0);
         name = reader.ReadString(1);
      }
   }
}