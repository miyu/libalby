using System;
using System.Collections.Generic;
using System.ComponentModel;
using ItzWarty;
using ItzWarty.Services;
using NLog;
using Shade.Server.Accounts.DataTransferObjects;
using Shade.Server.Accounts.Distributed;
using Shade.Server.SpecializedCache;

namespace Shade.Server.Accounts
{
    public class AccountServiceImpl : AccountService
    {
       private static Logger logger = LogManager.GetCurrentClassLogger();

       private readonly PlatformCacheService platformCacheService;
       private readonly SpecializedCacheService specializedCacheService;

       private readonly Dictionary<string, ShardAccountService> shardAccountServicesByShardId = new Dictionary<string, ShardAccountService>();

       public AccountServiceImpl(IServiceLocator serviceLocator, PlatformConfiguration platformConfiguration, PlatformCacheService platformCacheService, SpecializedCacheService specializedCacheService)
       {  
          serviceLocator.RegisterService(typeof(AccountService), this);

          this.platformCacheService = platformCacheService;
          this.specializedCacheService = specializedCacheService;

          foreach (var shardConfiguration in platformConfiguration.ShardConfigurations) {
             InitializeShard(shardConfiguration);
          }
       }

       private void InitializeShard(ShardConfiguration shardConfiguration)
       {
          string shardId = shardConfiguration.ShardId;
          var shardAccountService = new ShardAccountServiceImpl(shardId, platformCacheService, specializedCacheService);
          shardAccountServicesByShardId.Add(shardId, shardAccountService);
       }

       public AccountIdV1 CreateAccount(string shardId, string username)
       {
          var shardAccountService = shardAccountServicesByShardId.GetValueOrDefault(shardId);

          AccountIdV1 id = null;
          if (shardAccountService != null)
          {
             id = shardAccountService.CreateAccount(username);
          }
          logger.Info("Create account " + username + " on shard " + shardId);
          return id;
       }
    }
}
