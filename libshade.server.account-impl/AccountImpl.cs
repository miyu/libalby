namespace Shade.Server.Accounts
{
   public class AccountImpl : Account
   {
      private AccountKey accountKey;
      private string username;

      public AccountImpl(AccountKey accountKey, string username)
      {
         this.accountKey = accountKey;
         this.username = username;
      }

      public AccountKey Key { get { return accountKey; } }
      public string Username { get { return username; } }
   }
}