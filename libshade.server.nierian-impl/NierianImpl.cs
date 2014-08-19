using Shade.Server.Accounts;

namespace Shade.Server.Nierians
{
   public class NierianImpl : Nierian
   {
      private readonly NierianKey nierianKey;
      private string name;

      public NierianImpl(NierianKey niarianKey, string nierianName)
      {
         this.nierianKey = niarianKey;
         this.name = nierianName;
      }

      public NierianKey Key { get { return nierianKey; } }
      public string Name { get { return name; } }
   }
}