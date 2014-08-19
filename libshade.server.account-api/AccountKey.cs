using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shade.Server.Accounts
{
   public interface AccountKey
   {
      string ShardId { get; }
      uint AccountId { get; }
   }
}
