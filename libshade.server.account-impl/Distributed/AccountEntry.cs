using Dargon.PortableObjects;

namespace Shade.Server.Accounts.Distributed
{
   public class AccountEntry : IPortableObject
   {
      private AccountKey accountKey;
      private string username;

      public AccountEntry(AccountKey accountKey, string username)
      {
         this.accountKey = accountKey;
         this.username = username;
      }

      public AccountKey Key { get { return accountKey; } }
      public string Username { get { return username; } }

      public void Serialize(IPofWriter writer)
      {
         writer.WriteObject(0, accountKey); 
         writer.WriteString(1, username);
      }

      public void Deserialize(IPofReader reader)
      {
         accountKey = reader.ReadObject<AccountKey>(0);
         username = reader.ReadString(1);
      }
   }
}