using Shade.Server.Accounts.DataTransferObjects;

namespace Shade.Server.Accounts
{
   public interface ShardAccountService
   {
      AccountIdV1 CreateAccount(string username);
   }
}