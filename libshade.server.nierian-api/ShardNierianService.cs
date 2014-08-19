using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shade.Server.Accounts;

namespace Shade.Server.Nierians
{
   public interface ShardNierianService
   {
      NierianKey CreateNierian(AccountKey accountKey, string nierianName);
   }
}
