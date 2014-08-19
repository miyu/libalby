namespace Shade.Server.Accounts
{
   public interface ShardAccountService
   {
      AccountKey CreateAccount(string username);
   }
}