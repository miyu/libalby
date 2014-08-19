using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shade.Server
{
   public class PlatformConfiguration
   {
      private readonly ShardConfiguration[] shardConfigurations;

      public PlatformConfiguration(ShardConfiguration[] shardConfigurations) { this.shardConfigurations = shardConfigurations; }

      public ShardConfiguration[] ShardConfigurations { get { return shardConfigurations; } }
   }
}
