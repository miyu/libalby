using System.Collections.Generic;
using Shade.Server.Accounts;
using Shade.Server.Accounts.DataTransferObjects;
using Shade.Server.Nierians.DTOs;

namespace Shade.Server.Nierians
{
    public interface NierianService
    {
       NierianIdV1 CreateNierian(string shardId, ulong accountId, string nierianName);
       IEnumerable<NierianIdV1> EnumerateNieriansByAccount(string shardId, ulong accountId);
       void SetNierianName(string shardId, ulong accountId, ulong nierianId, string name);
    }
}
