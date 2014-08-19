namespace Shade.Server.Accounts
{
   public interface AccountService
   {
      AccountKey CreateAccount(string shardId, string username);
   }
}
