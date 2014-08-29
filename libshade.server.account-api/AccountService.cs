using Shade.Server.Accounts.DataTransferObjects;

namespace Shade.Server.Accounts
{
   public interface AccountService
   {
      AccountIdV1 CreateAccount(string shardId, string username);
   }
}
