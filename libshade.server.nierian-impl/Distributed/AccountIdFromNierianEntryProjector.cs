using Dargon.Distributed;

namespace Shade.Server.Nierians.Distributed
{
   public class AccountIdFromNierianEntryProjector : ICacheProjector<NierianKey, NierianEntry, ulong>
   {
      public ulong Project(IEntry<NierianKey, NierianEntry> entry) { return entry.Key.AccountId; }
   }
}
