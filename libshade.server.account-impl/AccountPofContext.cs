using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dargon.PortableObjects;
using Shade.Server.Accounts.Distributed;

namespace Shade.Server.Accounts
{
   public class AccountPofContext : PofContext
   {
      // 20000 - 29999
      public AccountPofContext()
      {
         this.RegisterPortableObjectType(20000, typeof(AccountEntry));
         this.RegisterPortableObjectType(20001, typeof(AccountKey));
         this.RegisterPortableObjectType(20002, typeof(ShardAccountCache.AccountCreationProcessor));
      }
   }
}
