namespace Shade.Server.Accounts
{
   public interface Account
   {
      AccountKey Key { get; }
      string Username { get; }
   }
}