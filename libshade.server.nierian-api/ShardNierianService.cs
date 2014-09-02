using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shade.Server.Accounts;
using Shade.Server.Nierians.DTOs;

namespace Shade.Server.Nierians
{
   public interface ShardNierianService
   {
      NierianIdV1 CreateNierian(ulong accountId, string nierianName);
      IEnumerable<NierianIdV1> EnumerateNieriansByAccount(ulong accountId);
      void SetNierianName(ulong accountId, ulong nierianId, string name);
   }
}
