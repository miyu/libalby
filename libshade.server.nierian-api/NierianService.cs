using System.Collections.Generic;
using Shade.Server.Accounts;

namespace Shade.Server.Nierians
{
    public interface NierianService
    {
       NierianKey CreateNierian(AccountKey accountKey, string nierianName);
       IReadOnlyCollection<Nierian> GetNieriansByAccount(AccountKey accountKey);
       void SetNierianName(Nierian nierian, string name);
    }
}
