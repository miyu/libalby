using System;
using System.Collections.Generic;
using System.ComponentModel;
using ItzWarty;

namespace Shade.Server.Accounts
{
    public class AccountServiceImpl : AccountService
    {
       private Dictionary<string, ShardAccountService> shardAccountServicesByShardId = new Dictionary<string, ShardAccountService>(); 

       public AccountServiceImpl(ShadeServiceLocator shadeServiceLocator, PlatformConfiguration platformConfiguration)
       {  
          shadeServiceLocator.RegisterService(typeof(AccountService), this);

          foreach (var shardConfiguration in platformConfiguration.ShardConfigurations) {
             InitializeShard(shardConfiguration);
          }
       }

       private void InitializeShard(ShardConfiguration shardConfiguration)
       {
          string shardId = shardConfiguration.ShardId;
          var shardAccountService = new ShardAccountServiceImpl(shardId);
          shardAccountServicesByShardId.Add(shardId, shardAccountService);
       }

       public AccountKey CreateAccount(string shardId, string username)
       {
          var shardAccountService = shardAccountServicesByShardId.GetValueOrDefault(shardId);

          AccountKey result = null;
          if (shardAccountService != null) {
             result = shardAccountService.CreateAccount(username);
          }
          return result;
       }
    }
}
